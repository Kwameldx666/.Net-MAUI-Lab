using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using Family_Rewards_Bank.Data;
using Family_Rewards_Bank.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Family_Rewards_Bank.ViewModels
{
    public partial class OrganizerViewModel : ObservableObject
    {
        [ObservableProperty]
        private DateTime selectedDate = DateTime.Now;

        [ObservableProperty]
        private EventItem selectedEvent;

        private readonly TodoDatabase _dbConnection;
        public ObservableCollection<EventItem> Events { get; private set; } = new();
        public OrganizerViewModel()
        {

            _dbConnection = new TodoDatabase();
             LoadItems();
        }
        [RelayCommand]
        private async Task LoadItems()
        {
            var items = await _dbConnection.GetItemsAsync();

            Events.Clear();
            foreach (var item in items)
                Events.Add(item);
        }

        [RelayCommand]
        public async Task AddEventAsync()
        {
            await Shell.Current.GoToAsync("//AddEventPage");
        }
        [RelayCommand]
        public void RemoveEvent(EventItem eventItem)
        {
            if (eventItem != null)
            {
                try
                {
                    // Удаляем из базы
                    _dbConnection.DeleteItem(eventItem.Id);

                    // Удаляем из ObservableCollection
                    Events.Remove(eventItem);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }


        [RelayCommand]
        public async Task UpdateEventAsync(EventItem eventItem)
        {

            if (eventItem == null)

                return;
            }

            // Store the event for the UpdateEventPage to access
            EventService.SetCurrentEvent(SelectedEvent);

            var page = new UpdateEventPage(eventItem);
            await Application.Current.MainPage.Navigation.PushAsync(page);
        }

    }
}
