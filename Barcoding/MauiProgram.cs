using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using Mopups.Hosting;
using Barcoding.ViewModels;
using ZXing.Net.Maui.Controls;

namespace Barcoding
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureMopups()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Roboto-Bold.ttf", "RobotoBold");
                    fonts.AddFont("Roboto-Medium.ttf", "RobotoMed");
                    fonts.AddFont("Roboto-Regular.ttf", "RobotoReg");
                    fonts.AddFont("fontello.ttf", "Icons");
                })

                .UseBarcodeReader();


                ;

//#if DEBUG
    		builder.Logging.AddDebug();

            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<QRScannerViewModel>();

            builder.Services.AddSingleton<NewsPage>();
            builder.Services.AddSingleton<NewsPageViewModel>();

            builder.Services.AddSingleton<ProductPage>();
            builder.Services.AddSingleton<ProductViewModel>();

            builder.Services.AddSingleton<RateMore>();
            builder.Services.AddSingleton<NewsViewModel>();

            builder.Services.AddSingleton<UserRatingPage>();
            builder.Services.AddSingleton<UserRatingModel>();

            builder.Services.AddSingleton<FullNewsDisplayer>();
            



//#endif

            return builder.Build();
        }
    }
}
