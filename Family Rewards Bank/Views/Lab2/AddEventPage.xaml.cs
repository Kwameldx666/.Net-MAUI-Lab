using Family_Rewards_Bank.ViewModels;

namespace Family_Rewards_Bank
{
    public partial class AddEventPage : ContentPage
    {
        public AddEventPage()
        {
            InitializeComponent();
            BindingContext = new AddEventViewModel();
        }
    }
}