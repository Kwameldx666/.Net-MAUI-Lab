using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using Family_Rewards_Bank.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Family_Rewards_Bank.ViewModels
{
    public partial class OrganizerViewModel : ObservableObject, IRecipient<ValueChangedMessage<EventItem>>
    {
        [ObservableProperty]
        private DateTime selectedDate = DateTime.Now;

        [ObservableProperty]
        private EventItem selectedEvent;

        public ObservableCollection<EventItem> Events { get; } = new();

        public OrganizerViewModel()
        {
            // Подписка на сообщения
            WeakReferenceMessenger.Default.Register(this);
        }

        // сюда прилетает новое или обновленное событие
        public void Receive(ValueChangedMessage<EventItem> message)
        {
            var eventItem = message.Value;

            // Проверяем, есть ли событие с таким Id
            var existingEvent = Events.FirstOrDefault(e => e.Id == eventItem.Id);

            if (existingEvent != null)
            {
                // Обновляем в коллекции
                int index = Events.IndexOf(existingEvent);
                Events[index] = eventItem;
            }
            else
            {
                // Новое событие
                Events.Add(eventItem);
            }
        }

        [RelayCommand]
        private async Task AddEventAsync()
        {
            await Shell.Current.GoToAsync(nameof(AddEventPage));
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

            // Передаём событие через параметры Shell
            var route = $"{nameof(UpdateEventPage)}?id={SelectedEvent.Id}";
            await Shell.Current.GoToAsync(route);
        }
    }
}
