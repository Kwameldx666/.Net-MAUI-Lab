using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Family_Rewards_Bank.Models;
using Microsoft.Maui.Controls;
using System;
using System.Threading.Tasks;


namespace Family_Rewards_Bank.ViewModels
{
    public partial class AddEventViewModel : ObservableObject
    {
        [ObservableProperty]
        private DateTime date = DateTime.Now;

        [ObservableProperty]
        private TimeSpan time = DateTime.Now.TimeOfDay;

        [ObservableProperty]
        private string description;

        [ObservableProperty]
        private string location;

        [RelayCommand]
        private async Task SaveAsync()
        {
            if (string.IsNullOrWhiteSpace(Description))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Please enter a description for the event.", "OK");
                return;
            }

            var newEvent = new EventItem
            {
                Date = Date,
                Time = Time,
                Description = Description,
                Location = Location
            };

            // ⚠️ Здесь можно вызвать сервис или Messenger, если нужно передать данные дальше
            await Application.Current.MainPage.DisplayAlert("Success", "Event saved!", "OK");

            await Application.Current.MainPage.Navigation.PopAsync();
        }

        [RelayCommand]
        private async Task CancelAsync()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
