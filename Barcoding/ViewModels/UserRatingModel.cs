using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Barcoding.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Globalization;
using System.Windows.Input;
using CommunityToolkit.Maui.Core;

namespace Barcoding.ViewModels
{

    [QueryProperty("Code", "Text")]

    public partial class UserRatingModel : CommunityToolkit.Mvvm.ComponentModel.ObservableObject
    {
        [ObservableProperty] string code;
        [ObservableProperty] int id;
        [ObservableProperty] string newsDate;
        [ObservableProperty] string newsName;
        [ObservableProperty] string fullNews1;
        [ObservableProperty] string source;

        [ObservableProperty] string imagecode1;
        [ObservableProperty] string imagecode2;

        [ObservableProperty]
        private bool isExpanded;

        public ICommand ToggleExpandCommand { get; }

        [ObservableProperty] ObservableCollection<string> ingList;

        public ObservableRangeCollection<Models.News> NewsList { get; set; }

        public AsyncCommand RefreshCommand { get; }

        public UserRatingModel()
        {

            ToggleExpandCommand = new RelayCommand(ToggleExpand);

            NewsList = new ObservableRangeCollection<Models.News>();

            RefreshCommand = new AsyncCommand(Refresh);

            Refresh();
        }


       

        [RelayCommand]
        async Task BackToScanner()
        {
            await Shell.Current.GoToAsync("..", true);
        }

        [RelayCommand]
        async Task FullNews()
        {
            //await Shell.Current.GoToAsync(nameof(FullNewsDisplayer), true);


        }

        private void ToggleExpand()
        {
            IsExpanded = !IsExpanded;
        }

        async Task Refresh()
        {

            NewsList.Clear();

            var news = await CompositionService.GetNews();

            foreach (var item in news)
            {
                if ( item.Id == Convert.ToInt32(Code))
                {
                    item.FormatedFullNews = item.FullNews;
                    DateTime UnTick = new DateTime(item.Date.Ticks, DateTimeKind.Utc);
                    item.FormatedDate = UnTick.ToString("dd MMMM, HH:mm yyyy", new CultureInfo("ru-RU"));
                    NewsList.Add(item);
                }                   
            }

            
            var element = new Models.News(); // или new YourNewsClassName();
            element.Source = "Вологодская О.Д.";
            element.NewsName = "Почему подняли цену ?";
            element.FormatedFullNews = "Превосходный товар, беру его постоянно, но помоему совершенно необоснованно сильно подняли цену, очень интересно, в связи с чем";
            element.Date = DateTime.Now; // Не забудьте установить дату, если она нужна
            element.FormatedDate = element.Date.ToString("dd MMMM, HH:mm yyyy", new CultureInfo("ru-RU"));

            NewsList.Add(element);
            



        }
    }
}
