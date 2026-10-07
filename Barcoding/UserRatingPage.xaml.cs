using Barcoding.ViewModels;
namespace Barcoding;

public partial class UserRatingPage : ContentPage
{
	public UserRatingPage( UserRatingModel vm )
	{
		InitializeComponent();
        BindingContext = vm;
    }

    private async void OnNavigateButtonClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter != null)
        {
            int id = Convert.ToInt32(button.CommandParameter.ToString());

            // Создаем НОВЫЙ ViewModel для новой страницы
            var newViewModel = new UserRatingModel
            {
                Id = id,
                // Установите другие свойства если нужно
            };

            // Создаем страницу с новым ViewModel
            //var page = new UserRatingPage(newViewModel, id);

            //await Navigation.PushAsync(page);
        }
    }

}