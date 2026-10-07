using Barcoding.ViewModels;

namespace Barcoding;

public partial class NewsPage : ContentPage
{
	public NewsPage( NewsPageViewModel vm )
	{
		InitializeComponent();
		BindingContext = vm;
	}

    private async void OnNavigateButtonClicked(object sender, EventArgs e)
    {
        var button = sender as Button;

        var parameter = button?.CommandParameter;

        string stringid = button.CommandParameter.ToString() ;

        int id = Convert.ToInt32(stringid);

        await Navigation.PushAsync(new FullNewsDisplayer( BindingContext as NewsPageViewModel, id ) );
    }

    /*
    protected override void OnAppearing()
    {
        base.OnAppearing();


        StatusBar.StatusBarColor = Color.FromArgb("#00AD1C") ;

    }

    protected override void OnDisappearing()
    {
        StatusBar.StatusBarColor = Color.FromArgb("#00AD1C");
    
    }
    */
}