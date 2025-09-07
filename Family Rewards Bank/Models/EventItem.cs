using CommunityToolkit.Mvvm.ComponentModel;
using System;
using SQLite;
namespace Family_Rewards_Bank.Models
{
    public partial class EventItem : ObservableObject
    {
        [PrimaryKey]
        public Guid Id { get; set; } = Guid.NewGuid();

        [ObservableProperty]
        private DateTime date;

        [ObservableProperty]
        private TimeSpan time;

        [ObservableProperty]
        private string description;

        [ObservableProperty]
        private string location;
        public bool HasLocation => !string.IsNullOrWhiteSpace(Location);
    }
}
