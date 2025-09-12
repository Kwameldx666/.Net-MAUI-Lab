using Family_Rewards_Bank.ViewModels;

namespace Family_Rewards_Bank
{
    public partial class RequestPage : ContentPage
    {
        public RequestPage()
        {
            InitializeComponent();
            BindingContext = new RequestViewModel();
        }
    }
}