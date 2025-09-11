using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Family_Rewards_Bank.ViewModels
{
    public partial class MainPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private string selectedCamera = "Back";

        [ObservableProperty]
        private string searchText;

        // Команда для открытия Organizer
        [RelayCommand]
        private async Task OpenOrganizer()
        {
            await Shell.Current.GoToAsync(nameof(Organizer));
        }

        // Команда для поиска в Google
        [RelayCommand]
        private async Task SearchInGoogle()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                await Shell.Current.DisplayAlert("Error", "Please enter something to search", "OK");
                return;
            }

            string url = $"https://www.google.com/search?q={Uri.EscapeDataString(SearchText)}";
            await Browser.Default.OpenAsync(url);
        }

        // Команда для показа alert
        [RelayCommand]
        private async Task ShowAlert()
        {
            await Task.Delay(10000);
            await Shell.Current.DisplayAlert("Show alert", "Alert notification", "Meow");
        }

        // Команда для фотографии
        [RelayCommand]
        private async Task TakePhoto()
        {
            try
            {
                if (!await RequestCameraPermissionAsync())
                {
                    await Shell.Current.DisplayAlert("Ошибка", "Доступ к камере запрещён", "OK");
                    return;
                }

                if (MediaPicker.Default.IsCaptureSupported)
                {
                    var photo = await MediaPicker.Default.CapturePhotoAsync();
                    if (photo != null)
                    {
                        var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
                        using var stream = await photo.OpenReadAsync();
                        using var newStream = File.OpenWrite(newFile);
                        await stream.CopyToAsync(newStream);

                        await Shell.Current.Navigation.PushAsync(new PhotoPage(newFile));
                    }
                }
                else
                {
                    await Shell.Current.DisplayAlert("Ошибка", "На устройстве недоступна камера", "OK");
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Ошибка", ex.Message, "OK");
            }
        }

        private async Task<bool> RequestCameraPermissionAsync()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.Camera>();
            if (status != PermissionStatus.Granted)
                status = await Permissions.RequestAsync<Permissions.Camera>();

            return status == PermissionStatus.Granted;
        }

        // Метод для выбора камеры
        public void CameraCheckedChanged(string camera)
        {
            SelectedCamera = camera;
            Console.WriteLine($"Выбрана камера: {SelectedCamera}");
        }
    }
}
