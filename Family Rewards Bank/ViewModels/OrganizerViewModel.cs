using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Family_Rewards_Bank.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Family_Rewards_Bank.ViewModels
{
    public partial class OrganizerViewModel : ObservableObject, IRecipient<EventItem>
    {
        [ObservableProperty]
        private DateTime selectedDate = DateTime.Now;

        [ObservableProperty]
        private EventItem selectedEvent;

        public ObservableCollection<EventItem> Events { get; } = new();

        public OrganizerViewModel()
        {
            // подписка на сообщения
            WeakReferenceMessenger.Default.Register(this);
        }

        // сюда прилетит новое событие
        public void Receive(EventItem message)
        {
            Events.Add(message);
        }

        [RelayCommand]
        private async Task AddEventAsync()
        {
            await App.Current.MainPage.Navigation.PushAsync(new AddEventPage());
        }

        [RelayCommand]
        private void RemoveEvent()
        {
            if (SelectedEvent != null)
                Events.Remove(SelectedEvent);
        }

        [RelayCommand]
        private async Task UpdateEventAsync()
        {
            if (SelectedEvent == null)
                return;

            var updatePage = new UpdateEventPage(SelectedEvent);
            await App.Current.MainPage.Navigation.PushAsync(updatePage);
        }
    }
}
