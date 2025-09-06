using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
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

            // Отправляем сообщение об изменении коллекции
            WeakReferenceMessenger.Default.Send(new ValueChangedMessage<EventItem>(newEvent));

            await Application.Current.MainPage.DisplayAlert("Success", "Event saved!", "OK");
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        private async Task CancelAsync()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
