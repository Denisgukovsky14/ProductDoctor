using Barcoding.ViewModels;

namespace Barcoding;

public partial class FullNewsDisplayer : ContentPage
{
	public FullNewsDisplayer( NewsPageViewModel vm, int id )
	{
		InitializeComponent();
		BindingContext = vm;

        var CurIdList = vm.NewsList.FirstOrDefault(news => news.Id == id);

        NewsTitle.Text = CurIdList.NewsName;

        NewsBlock.Text = "  " + CurIdList.FullNews;

        Image1.Source = "i" + CurIdList.Image1Code + ".jpg";
        Image2.Source = "i" + CurIdList.Image2Code + ".jpg";

        //int SpaceIndex = CurIdList.FullNews.Length / 2 + (CurIdList.FullNews.Substring( CurIdList.FullNews.Length/2 )).IndexOf(" ") ;

        //NewsBlock1.Text = CurIdList.FullNews.Substring( 1, SpaceIndex ) ;
        //NewsBlock2.Text = CurIdList.FullNews.Substring( SpaceIndex );


        // ƒобавить еще блоки новости и картинки


    }

    /*
    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
    */

    
}