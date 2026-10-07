using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MvvmHelpers;
using MvvmHelpers.Commands;
using Barcoding.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
//using System.Xml;


namespace Barcoding.ViewModels;

[QueryProperty("Text", "Text")]


public partial class NewsViewModel : CommunityToolkit.Mvvm.ComponentModel.ObservableObject
{

    [ObservableProperty] public string text;

    [ObservableProperty] public float productRating;

    [ObservableProperty] public string wtm1;

    [ObservableProperty] public string wtm2;

    [ObservableProperty] public string imgcode1;

    [ObservableProperty] public string imgcode2;

    


    public AsyncCommand RefreshCommand { get; }

    // Узнать чем RangeCollection Отличается от обычного Asynclist, может есть какая то разница.

    public ObservableRangeCollection<Models.Rating> Ratings { get; set; }

    [ObservableProperty] ObservableCollection<Models.Rating> items;

    public NewsViewModel()
    {


        


        Ratings = new ObservableRangeCollection<Models.Rating>();

        RefreshCommand = new AsyncCommand(Refresh);

        Items = new ObservableCollection<Models.Rating>();



    }

    [RelayCommand]
    async Task GoBack()
    {
        await Shell.Current.GoToAsync("..", true);
    }

    partial void OnTextChanged(string value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            Refresh();
        }
    }

    async Task Refresh()
    {
        Ratings.Clear();

        Items.Clear();

        if (Text != null)
        {

            // Вот здесь нужно изменить логику - Получать 3 компнента с оценкой 4 - 5 от роскачества в КАТЕГОРИИ
            // Т.Е. Добавить категорию, и так под сосисками, будут еще 3 вида сосисок с оценками

            var product = await CompositionService.GetProduct(Text);

            string c = product.category;
            var topThree = await CompositionService.TopThree( c );

            // удаляем себя же перед показом
            topThree = topThree.Where(item => item.Barcode != Text).ToList();

            // Очищаем коллекцию и добавляем новые элементы
            Items.Clear();
            foreach (var rating in topThree)
            {

                //Math.Floor(ProductRating);
                
                Wtm1 = "   " + rating.WhyThisMark;
                Wtm2 = "   " + rating.WhyThisMark2;

                Imgcode1 = "i" + rating.Image1Code + ".jpg";
                Imgcode2 = "i" + rating.Image2Code + ".jpg";


                rating.WhyThisMark = await CompositionService.GetProductName(rating.Barcode) ;
                Items.Add(rating);
            }

            /*
            var temprate = await CompositionService.GetRate(Text);

            Wtm1 = "   " + temprate.WhyThisMark;
            Wtm2 = "   " + temprate.WhyThisMark2;

            Imgcode1 = "i" + temprate.Image1Code + ".png" ;
            Imgcode2 = "i" + temprate.Image2Code + ".png";

            Ratings.Add(temprate);
            Items.Add(temprate);
            */

            //Items.Add(temprate);

        }
    }


}

