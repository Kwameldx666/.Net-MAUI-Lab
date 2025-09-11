using Family_Rewards_Bank.ViewModels;

namespace Family_Rewards_Bank.Views.Lab3;

public partial class WebServiceEmulation : ContentPage
{
	public WebServiceEmulation()
	{
		InitializeComponent();
		BindingContext = new WebServiceEmulationViewModel();

    }
}