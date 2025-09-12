using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;

namespace Family_Rewards_Bank.ViewModels
{
    public partial class RequestViewModel : ObservableObject
    {
        [ObservableProperty]
        private string? name;

        [ObservableProperty]
        private string? illness;

        [ObservableProperty]
        private string? location;

        [ObservableProperty]
        private string? description;

        [RelayCommand]
        private async Task MarkAsUrgent()
        {
            // Validate that required fields are filled
            if (string.IsNullOrWhiteSpace(Name) || 
                string.IsNullOrWhiteSpace(Illness) || 
                string.IsNullOrWhiteSpace(Location))
            {
                await Shell.Current.DisplayAlert("Error", 
                    "Please fill in all required fields (Name, Disease, Location)", 
                    "OK");
                return;
            }

            // For now, just show a debug message
            await Shell.Current.DisplayAlert("Very Urgent Request", 
                $"URGENT MEDICAL REQUEST SUBMITTED:\n\nName: {Name}\nDisease: {Illness}\nLocation: {Location}\nDescription: {Description ?? "No additional details provided"}\n\nThis request has been marked as VERY URGENT and will be processed immediately!", 
                "OK");

            // Clear the form after submission
            ClearForm();
        }

        [RelayCommand]
        private async Task SubmitRequest()
        {
            // Validate that required fields are filled
            if (string.IsNullOrWhiteSpace(Name) || 
                string.IsNullOrWhiteSpace(Illness) || 
                string.IsNullOrWhiteSpace(Location))
            {
                await Shell.Current.DisplayAlert("Error", 
                    "Please fill in all required fields (Name, Disease, Location)", 
                    "OK");
                return;
            }

            // For now, just show a debug message
            await Shell.Current.DisplayAlert("Request Submitted", 
                $"MEDICAL REQUEST SUBMITTED:\n\nName: {Name}\nDisease: {Illness}\nLocation: {Location}\nDescription: {Description ?? "No additional details provided"}\n\nYour request has been submitted and will be processed soon.", 
                "OK");

            // Clear the form after submission
            ClearForm();
        }

        private void ClearForm()
        {
            Name = string.Empty;
            Illness = string.Empty;
            Location = string.Empty;
            Description = string.Empty;
        }
    }
}