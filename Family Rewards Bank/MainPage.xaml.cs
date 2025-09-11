using Family_Rewards_Bank.ViewModels;
using Microsoft.Maui.Controls;

namespace Family_Rewards_Bank
{
    public partial class MainPage : ContentPage
    {
        private MainPageViewModel vm;

        public MainPage()
        {
            InitializeComponent();
            vm = new MainPageViewModel();
            BindingContext = vm;
        }

        private void Camera_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (sender is RadioButton rb && e.Value)
            {
                vm.CameraCheckedChanged(rb.Content.ToString());
            }
        }
    }
}
