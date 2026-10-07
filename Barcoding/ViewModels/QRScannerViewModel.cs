using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Barcoding.ViewModels
{
    public partial class QRScannerViewModel : ObservableObject
    {
        public QRScannerViewModel()
        {

            // ПРИМЕЧАНИЕ - блоки не нужно выполнять попрогонно, достаточно просто один раз все раскомментировать и все будет работать

            // Если захочется Удалить базу данных из внутреннего хранилища, то сделать это надо здесь
            
            var databaseDirectory = Path.Combine(FileSystem.AppDataDirectory);
            if (Directory.Exists(databaseDirectory))
            {
                var files = Directory.GetFiles(databaseDirectory);
                foreach (var file in files)
                {
                    var filePath = Path.Combine(databaseDirectory, "ProductDataBase.db");

                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }

                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                }
            }
            
            
        }

        [RelayCommand]
        async Task LoadNews()
        {
            await Shell.Current.GoToAsync(nameof(NewsPage), true );
            
        }

        [RelayCommand]
        async Task LoadProduct()
        {
            await Shell.Current.GoToAsync(nameof(ProductPage), true);
            //await Shell.Current.GoToAsync($"{nameof(ProductPage)}?code={"13454643547798747"}", true);

        }

        [RelayCommand]
        async Task MoreRate()
        {
            //var Page = new ProductPage("123");

            //await Shell.Current.GoToAsync(nameof(ProductPage), true);

            
        }


    }
}
