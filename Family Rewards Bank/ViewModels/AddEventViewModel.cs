using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using Family_Rewards_Bank.Data;
using Family_Rewards_Bank.Models;
using System;
using System.Threading.Tasks;

namespace Family_Rewards_Bank.ViewModels
{
    public partial class AddEventViewModel : ObservableObject
    {
        public AddEventViewModel()
        {
            db = new();
        }
        [ObservableProperty]
        private DateTime date = DateTime.Now;

        [ObservableProperty]
        private TimeSpan time = DateTime.Now.TimeOfDay;

        [ObservableProperty]
        private string? description;

        [ObservableProperty]
        private string? location;

        private readonly TodoDatabase db;


        [RelayCommand]
        public async Task Save()
        {
            var newEvent = new EventItem
            {
                Id = Guid.NewGuid(),
                Date = Date,     // Date уже DateTime
                Time = Time,     // Time уже TimeSpan
                Description = Description,
                Location = Location
            };
            try
            {
                await db.SaveItem(newEvent);
            }
            catch (Exception ex)
            {
               await Application.Current.MainPage.DisplayAlert("Mistake", ex.Message,"X");
            }


            await Shell.Current.GoToAsync("//Organizer");

        
        }

        [RelayCommand]
        public async Task Cancel()
        {

            await Shell.Current.GoToAsync("//Organizer");

        }
    }
}
