using Barcoding.ViewModels;

namespace Barcoding;

public partial class RateMore : ContentPage
{
	public RateMore( NewsViewModel vm )
	{
		InitializeComponent();
		BindingContext = vm; 
	}
}