using System;
using Family_Rewards_Bank.Models;
using Microsoft.Maui.Controls;

namespace Family_Rewards_Bank
{
    public partial class UpdateEventPage : ContentPage
    {
        private readonly EventItem _originalEvent;
        public event EventHandler<EventItem> EventUpdated;

        public UpdateEventPage(EventItem existingEvent)
        {
            InitializeComponent();

            _originalEvent = existingEvent;

            // ��������� ���� �� ������������� �������
            eventDescription.Text = existingEvent.Description;
            eventLocation.Text = existingEvent.Location;
            eventDate.Date = existingEvent.Date;
            eventTime.Time = existingEvent.Time;
        }

        private async void CancelButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(eventDescription.Text))
            {
                await DisplayAlert("Error", "Please enter a description.", "OK");
                return;
            }

            // ��������� ������ �������
            _originalEvent.Description = eventDescription.Text;
            _originalEvent.Location = eventLocation.Text;
            _originalEvent.Date = eventDate.Date;
            _originalEvent.Time = eventTime.Time;

            // ������� ������ EventItem
            EventUpdated?.Invoke(this, _originalEvent);

            await Navigation.PopAsync();
        }
    }
}
