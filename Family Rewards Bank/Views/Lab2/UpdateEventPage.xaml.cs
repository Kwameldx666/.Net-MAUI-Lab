using CommunityToolkit.Mvvm.Messaging;
using Family_Rewards_Bank.Models;
using Family_Rewards_Bank.ViewModels;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls;

namespace Family_Rewards_Bank
{
    [QueryProperty(nameof(EventId), "id")]
    public partial class UpdateEventPage : ContentPage
    {
        private UpdateEventViewModel _viewModel;

        public string EventId
        {
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    LoadEventById(value);
                }
            }
        }

        public UpdateEventPage()
        {
            InitializeComponent();
            _viewModel = new UpdateEventViewModel();
            BindingContext = _viewModel;
        }

        public UpdateEventPage(EventItem existingEvent) : this()
        {
            _viewModel.LoadEvent(existingEvent);
        }

        private void LoadEventById(string eventId)
        {
            // For now, we'll use the shared event via messaging system
            // In a real app, you'd get this from a database or service
            
            // Since we can't easily access the event from the organizer here,
            // we'll modify the approach to pass the event via a static property
            var eventItem = EventService.GetEventById(eventId);
            if (eventItem != null)
            {
                _viewModel.LoadEvent(eventItem);
            }
        }
    }

    // Simple service to store and retrieve events by ID
    public static class EventService
    {
        private static EventItem _currentEvent;

        public static void SetCurrentEvent(EventItem eventItem)
        {
            _currentEvent = eventItem;
        }

        public static EventItem GetEventById(string id)
        {
            return _currentEvent?.Id == id ? _currentEvent : null;
        }
    }

}