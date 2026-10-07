using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Controls.Shapes;
using Barcoding.ViewModels;
using Mopups.Services;
using ZXing;
using System.ComponentModel;
using Barcoding.Services;
using Barcoding.Models;

namespace Barcoding;

public partial class ProductPage : ContentPage
{
	public ProductPage( string code )
	{
		
		var vm = new ProductViewModel( Navigation, code );
		InitializeComponent();
        BindingContext = vm;

    }

	private void LookInfo( object sender, EventArgs e)
	{
        /*
        if( sender is Button)
        {
            Button button = (Button) sender;
        }
        */

        // Конструкция "Сопоставление типов" С# позволяет укоротить это выражение (сверху), укоротив это действие до одного

        if (sender is Button button && int.TryParse(button.CommandParameter.ToString(), out int result))
        {
            

            string info = button.CommandParameter.ToString() ;
            MopupService.Instance.PushAsync(new Decoding(info));
        }
        else
        {
            string info = "Здесь вы можете посмотреть состав продукта, красным цветом обозначены потенциально опасные для некоторых людей ингридиенты, кликните на ингридиент, чтобы узнать о нём подробнее";
            MopupService.Instance.PushAsync(new Decoding(info));
        }
	}

    private void IngInfo(object sender, TappedEventArgs e)
    {

        // сделать так, чтобы передавался заголовок в цвете и описание черным

        var ingredient = e.Parameter as Ingridients;

        string info = ingredient.Name;
        string color = ingredient.Color;


        if (info != "0" && color=="Red")
        {

            MopupService.Instance.PushAsync(new Decoding(info));
        }
    }

    //private async bool IsRed(string name)
    //{
    //    return await CompositionService.GetProductColor(name);
    //}

    protected override void OnAppearing()
    {

        base.OnAppearing();
        
    }

    private async void OnNavigateButtonClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter != null)
        {
            int id = Convert.ToInt32(button.CommandParameter.ToString());

            // Создаем НОВЫЙ ViewModel для новой страницы
            var newViewModel = new UserRatingModel
            {
                Code = Convert.ToString(id),
                // Установите другие свойства если нужно
            };

            // Создаем страницу с новым ViewModel
            //var page = new UserRatingPage(newViewModel, id);

            //await Navigation.PushAsync(page);
        }
    }


}