using Barcoding.ViewModels;

namespace Barcoding
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute( nameof(NewsPage), typeof(NewsPage) );
            Routing.RegisterRoute(nameof(FullNewsDisplayer), typeof(FullNewsDisplayer));
            Routing.RegisterRoute( nameof(ProductViewModel), typeof(ProductViewModel) );
            Routing.RegisterRoute(nameof(ProductPage), typeof(ProductPage));
            Routing.RegisterRoute(nameof(RateMore), typeof(RateMore));
            Routing.RegisterRoute(nameof(UserRatingPage), typeof(UserRatingPage));
            Routing.RegisterRoute(nameof(UserRatingModel), typeof(UserRatingModel));
        }
    }
}
