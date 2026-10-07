
using Microsoft.Maui.Controls;
using Mopups.Services;
using System.Globalization;
using Barcoding.Services;
using SQLitePCL;

namespace Barcoding;

public partial class Decoding 
{
    private bool _isSetup = false;
    public int id;

	public Decoding(string info)
	{
       
        InitializeComponent();

        if (int.TryParse(info.ToString(), out int result))
        {
            DecodingName.Text = "Отзыв о продукте";
            id = Convert.ToInt32(result);
            SetupReviewForm();
        }
        else if(info.ToString() == "Здесь вы можете посмотреть состав продукта, красным цветом обозначены потенциально опасные для некоторых людей ингридиенты, кликните на ингридиент, чтобы узнать о нём подробнее")
        {
            DecodingName.TextColor = Color.FromArgb("#000000");
            DecodingName.Text = info;
        }
        else
        {

            DecodingName.TextColor = Color.FromArgb("#FF0000");
            DecodingName.Text = info;
            GetDescByName(info);
        }

    }

  

    private void SetupReviewForm( )
    {
        if (_isSetup) return;

        // Показываем контейнер с формой отзыва
        ReviewContainer.IsVisible = true;

        RatingSlider.ValueChanged -= OnRatingSliderValueChanged;
        SubmitReviewButton.Clicked -= OnSubmitReviewClicked;

        // Настраиваем слайдер для отображения текущего значения
        RatingSlider.ValueChanged += OnRatingSliderValueChanged;

        // Устанавливаем обработчик для кнопки
        SubmitReviewButton.Clicked += OnSubmitReviewClicked;

        _isSetup = true;
    }

    private void OnRatingSliderValueChanged(object sender, ValueChangedEventArgs e)
    {
        // Обновляем текстовое представление оценки
        var value = (int)Math.Round(e.NewValue);
        RatingLabel.Text = $"{value}/5";
        RatingSlider.Value = value; // Фиксируем на целых значениях
    }


    private async void GetDescByName(string name)
    {
       
           DecodingDesc.Text = await CompositionService.GetProductDescription(name);
        
    }


    private async void OnSubmitReviewClicked(object sender, EventArgs e)
    {
        // Валидация полей
        if (string.IsNullOrWhiteSpace(UserNameEntry.Text))
        {
            await DisplayAlert("Ошибка", "Пожалуйста, введите ваше имя", "OK");
            return;
        }

        if (string.IsNullOrWhiteSpace(ReviewTitleEntry.Text))
        {
            await DisplayAlert("Ошибка", "Пожалуйста, введите заголовок отзыва", "OK");
            return;
        }

        if (string.IsNullOrWhiteSpace(ReviewTextEditor.Text))
        {
            await DisplayAlert("Ошибка", "Пожалуйста, напишите текст отзыва", "OK");
            return;
        }

        // Собираем данные отзыва
        var reviewData = new
        {
            UserName = UserNameEntry.Text.Trim(),
            Title = ReviewTitleEntry.Text.Trim(),
            Review = ReviewTextEditor.Text.Trim(),
            Rating = (int)RatingSlider.Value,
            Date = DateTime.Now
        };

        // ЛОГИКА ОТПРАВКИ ОТЗЫВА НА СЕРВЕР

        // Временная заглушка - показываем результат
        //await DisplayAlert("Спасибо!",
        //    $"Отзыв от {reviewData.UserName} с оценкой {reviewData.Rating}/5 успешно отправлен!",
        //    "OK");

        var element = new Models.News(); // или new YourNewsClassName();
        element.Id = id;
        element.Source = reviewData.UserName;
        element.NewsName = reviewData.Title ;
        element.FullNews = reviewData.Review ;
        element.Date = DateTime.Now; // Не забудьте установить дату, если она нужна
        element.FormatedDate = element.Date.ToString("dd MMMM, HH:mm yyyy", new CultureInfo("ru-RU"));
        await CompositionService.AddNews(element);


        // Очищаем форму после отправки
        //await MopupService.Instance.PopAsync();
        ClearReviewForm();
    }



    private async void ClearReviewForm()
    {
        RatingSlider.Value = 3;
        RatingLabel.Text = "3/5";
        UserNameEntry.Text = string.Empty;
        ReviewTitleEntry.Text = string.Empty;
        ReviewTextEditor.Text = string.Empty;
        ReviewContainer.IsVisible = false;
        DecodingName.Text = "Спасибо за отзыв!";
        _isSetup = false;
    }


    private async void Button_Clicked(object sender, EventArgs e)
    {
        RatingSlider.ValueChanged -= OnRatingSliderValueChanged;
        SubmitReviewButton.Clicked -= OnSubmitReviewClicked;
        await MopupService.Instance.PopAsync();
    }
}