using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using Family_Rewards_Bank.Data;
using Family_Rewards_Bank.Models;
using Microsoft.Maui.Controls;
using System;
using System.Threading.Tasks;

namespace Family_Rewards_Bank.ViewModels
{
    public partial class UpdateEventViewModel : ObservableObject
    {

        private readonly EventItem _originalEvent;
        private readonly TodoDatabase _todoDatabase;

        [ObservableProperty]
        private DateTime date = DateTime.Now;

        [ObservableProperty]
        private TimeSpan time = DateTime.Now.TimeOfDay;

        [ObservableProperty]
        private string description;

        [ObservableProperty]
        private string location;

        public UpdateEventViewModel()
        {
            // Parameterless constructor for Shell navigation
        }

        public UpdateEventViewModel(EventItem eventToUpdate) : this()
        {
            LoadEvent(eventToUpdate);
        }

        public void LoadEvent(EventItem eventToUpdate)
        {
            _originalEvent = eventToUpdate;

            Date = eventToUpdate.Date;
            Time = eventToUpdate.Time;
            Description = eventToUpdate.Description;
            Location = eventToUpdate.Location;
            _todoDatabase = new();
        }

        [RelayCommand]
        private async Task SaveAsync()
        {
            if (_originalEvent == null)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "No event selected for update.", "OK");
                return;
            }

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

            try
            {
                await _todoDatabase.SaveItem(_originalEvent);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Mistake", ex.Message, "X");
            }


            await Application.Current.MainPage.DisplayAlert("Success", "Event updated!", "OK");
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        private async Task CancelAsync()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
