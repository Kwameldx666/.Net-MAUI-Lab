using CommunityToolkit.Mvvm.Messaging;
using Family_Rewards_Bank.Models;
using Family_Rewards_Bank.ViewModels;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls;

namespace Family_Rewards_Bank
{
    public partial class UpdateEventPage : ContentPage
    {
        public UpdateEventPage(EventItem existingEvent)
        {
            InitializeComponent();
            BindingContext = new UpdateEventViewModel(existingEvent);
        }
    }

}