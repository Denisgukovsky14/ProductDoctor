using Barcoding.ViewModels;
using CommunityToolkit.Maui.Core.Platform;
using ZXing;

namespace Barcoding
{
    public partial class MainPage : ContentPage
    {

        //bool ScanEnable = true;

        public MainPage( QRScannerViewModel vm )
        {
            
            InitializeComponent();
            barcodeReader.Options = new ZXing.Net.Maui.BarcodeReaderOptions
            {
                
                //Formats = ZXing.Net.Maui.BarcodeFormats.All ,

                Formats = ZXing.Net.Maui.BarcodeFormat.Ean13,

                AutoRotate = true,
                Multiple = false,

            };

            //barcodeReader.IsDetecting = true;

            BindingContext = vm;

        }

        private async void barcodeReader_BarcodesDetected(object sender, ZXing.Net.Maui.BarcodeDetectionEventArgs e)
        {
            var first = e.Results?.FirstOrDefault();
            barcodeReader.IsDetecting = false;
            if (first != null)
            {
                barcodeReader.IsDetecting = false;

                Dispatcher.Dispatch(() =>
                {
                    //await ShowProduct(first.Value.ToString());

                    var productPage = new ProductPage(first.Value.ToString());

                    Navigation.PushAsync(productPage, true);

                    barcodeReader.IsDetecting = false;


                });

                //var productPage = new ProductPage( first.Value.ToString() );

                //await Navigation.PushAsync(productPage, true);

                //barcodeReader.IsDetecting = false;

                return;
            }


            

    }

        async Task ShowProduct( string code )
        {
            await Shell.Current.GoToAsync($"{nameof( ProductPage )}?code={code}", true);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            barcodeReader.IsDetecting = false;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            barcodeReader.IsDetecting = true;
        }

    }


    

    /*
    protected override void OnAppearing()
    {
        base.OnAppearing();

        //#00AD1C
        StatusBar.StatusBarColor = Color.FromArgb("#00AD1C");

    }
    */
}


