using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System.Collections.ObjectModel;

using Barcoding;
using Barcoding.Services;
using Mopups.Services;




namespace Barcoding.ViewModels
{
    public partial class ProductViewModel : CommunityToolkit.Mvvm.ComponentModel.ObservableObject
    {

        private readonly INavigation _navigation;

        public string CurrentCode;

        

        // Значения дли биндинга

        [ObservableProperty] int id;
        [ObservableProperty] string title;
        [ObservableProperty] string manrer;
        [ObservableProperty] string imagecode;
        [ObservableProperty] int rate;

        [ObservableProperty] string proteins;
        [ObservableProperty] string fats;
        [ObservableProperty] string carbohydrates;
        [ObservableProperty] string kilocalories;


        [ObservableProperty] ObservableCollection<string> comList;
        [ObservableProperty] ObservableCollection<string> ingList;

        public ObservableRangeCollection<Models.Compositions> Products { get; set; }
        public ObservableRangeCollection<Models.Ingridients> Ingridients { get; set; }

        public AsyncCommand RefreshCommand { get; }

        public AsyncCommand InitBaseCommand { get; }

        [ObservableProperty] ObservableCollection<string> nutritionalvalue;

        // Добавьте это свойство
        //public string IngredientsDisplay => string.Join(", ", Ingridients.Select(i => i.Name));

        public ObservableRangeCollection<object> DisplayItems { get; set; } = new();


        public ProductViewModel(INavigation navigation, string code )
        {

            this._navigation = navigation;
            LoadPage();

            CurrentCode = code;

            Products = new ObservableRangeCollection<Models.Compositions>();
            Ingridients = new ObservableRangeCollection<Models.Ingridients>();

            RefreshCommand = new AsyncCommand(Refresh);

            InitBaseCommand = new AsyncCommand(InitBase);

            Refresh();
        }

        async Task InitBase()
        {
            await CompositionService.Init();
        }

        [RelayCommand]
        async Task BackToScanner()
        {
            await Shell.Current.GoToAsync("..", true);
        }

        [RelayCommand]
        async Task MoreRate()
        {
            //await Shell.Current.GoToAsync(nameof(RateMore), true);
            await Shell.Current.GoToAsync($"{nameof(RateMore)}?Text={CurrentCode}",true);
        }

        async Task Refresh()
        {

            Products.Clear();
            Ingridients.Clear();
            DisplayItems.Clear(); // Очищаем новую коллекцию

            var products = await CompositionService.GetProduct(CurrentCode);

            string TempIngridients = products.Composition.ToString();
            ComList = new ObservableCollection<string>(TempIngridients.Split(','));

            for (int i = 0; i < ComList.Count; i++)
            {
                var element = await CompositionService.GetIngridient(ComList[i]);


                Ingridients.Add(element);
                DisplayItems.Add(element); // Добавляем ингредиент

                if (i < ComList.Count - 1) // Если не последний
                {
                    DisplayItems.Add(", "); // Добавляем запятую
                }
            }

            // Блок биндинга значений из базы в обр. свойства
            Products.Add(products);


            Id = Products.First().Id;
            Title = Products.First().Title;
            Manrer = Products.First().Manufacturer;

            Imagecode = "i" + Products.First().ImageCode + ".jpg";
            Rate = Products.First().ProductRating;

            Proteins = Products.First().Proteins + " Белков";
            Fats = Products.First().Fats + " Жиров";
            Carbohydrates = Products.First().Carbohydrates + " Углеводов";
            Kilocalories = Products.First().Kilocalories + " ККалорий";


            if (_navigation != null)
            {
                await Task.Delay(1000);
                UnLoadPage();
            }
        }

        [RelayCommand]
        async Task LoadUserRatings()
        {
            //await Shell.Current.GoToAsync(nameof(UserRatingPage), true);
            await Shell.Current.GoToAsync($"{nameof(UserRatingPage)}?Text={Id}", true);

    

        }

        public async Task LoadPage()
        {
            var loadpage = new BasicLoadPage(); // Создайте экземпляр вашей страницы
            await this._navigation.PushAsync(loadpage,false);
            
        }

        public async Task UnLoadPage()
        {
            await this._navigation.PopAsync();
        }

    }
}
