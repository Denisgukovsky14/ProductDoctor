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

namespace Barcoding.ViewModels
{
    public partial class NewsPageViewModel : CommunityToolkit.Mvvm.ComponentModel.ObservableObject
    {

        [ObservableProperty] string test = "123";

        [ObservableProperty] string newsDate ;
        [ObservableProperty] string newsName;
        [ObservableProperty] string fullNews1;
        [ObservableProperty] string source;

        [ObservableProperty] string imagecode1;
        [ObservableProperty] string imagecode2;

        [ObservableProperty] ObservableCollection<string> ingList;

        public ObservableRangeCollection<Models.News> NewsList { get; set; }

        public AsyncCommand RefreshCommand { get; }

        public NewsPageViewModel()
        {
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

        async Task Refresh()
        {

            NewsList.Clear();

            var news = await CompositionService.GetNews();

            foreach (var item in news)
            {
                item.FormatedFullNews = item.FullNews; //.Substring(0, 90) + "..." ;


                DateTime UnTick = new DateTime(item.Date.Ticks, DateTimeKind.Utc);

                item.FormatedDate = UnTick.ToString("dd MMMM, HH:mm yyyy", new CultureInfo("ru-RU"));

                NewsList.Add(item);
            }

            
            




        }
    }
}
