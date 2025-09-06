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
    public partial class UpdateEventViewModel : ObservableObject
    {
        private readonly EventItem _originalEvent;

        [ObservableProperty]
        private DateTime date;

        [ObservableProperty]
        private TimeSpan time;

        [ObservableProperty]
        private string description;

        [ObservableProperty]
        private string location;

        public UpdateEventViewModel(EventItem eventToUpdate)
        {
            _originalEvent = eventToUpdate;

            Date = eventToUpdate.Date;
            Time = eventToUpdate.Time;
            Description = eventToUpdate.Description;
            Location = eventToUpdate.Location;
        }

        [RelayCommand]
        private async Task SaveAsync()
        {
            if (string.IsNullOrWhiteSpace(Description))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Please enter a description for the event.", "OK");
                return;
            }

            // Обновляем объект
            _originalEvent.Date = Date;
            _originalEvent.Time = Time;
            _originalEvent.Description = Description;
            _originalEvent.Location = Location;

            // Отправляем сообщение о том, что событие изменилось
            WeakReferenceMessenger.Default.Send(new ValueChangedMessage<EventItem>(_originalEvent));

            await Application.Current.MainPage.DisplayAlert("Success", "Event updated!", "OK");
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        [RelayCommand]
        private async Task CancelAsync()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
