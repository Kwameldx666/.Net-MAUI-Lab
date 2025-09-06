using Microsoft.Maui;
using Microsoft.Maui.Controls;
using System;
using System.Threading.Tasks;

namespace Family_Rewards_Bank
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        private string selectedCamera = "Back";

        private void Camera_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (sender is RadioButton rb && e.Value)
            {
                selectedCamera = rb.Content.ToString();
                Console.WriteLine($"Выбрана камера: {selectedCamera}");
            }
        }

        private async Task<bool> RequestCameraPermissionAsync()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.Camera>();

            if (status != PermissionStatus.Granted)
            {
                status = await Permissions.RequestAsync<Permissions.Camera>();
            }

            return status == PermissionStatus.Granted;
        }

        private async void TakePhotoButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                // 1. Проверяем разрешение
                if (!await RequestCameraPermissionAsync())
                {
                    await DisplayAlert("Ошибка", "Доступ к камере запрещён", "OK");
                    return;
                }

                // 2. Проверяем, поддерживает ли устройство захват
                if (MediaPicker.Default.IsCaptureSupported)
                {
                    FileResult photo = await MediaPicker.Default.CapturePhotoAsync();

                    if (photo != null)
                       {
                        // Сохраним фото во временный файл
                        var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
                        using var stream = await photo.OpenReadAsync();
                        using var newStream = File.OpenWrite(newFile);
                        await stream.CopyToAsync(newStream);

                        // Откроем страницу для просмотра фото
                        await Navigation.PushAsync(new PhotoPage(newFile));
                    }
                }
                else
                {
                    await DisplayAlert("Ошибка", "На устройстве недоступна камера", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", ex.Message, "OK");
            }
        }




        private async void buttonSearchInGoogleCheck_Clicked(object sender, EventArgs e)
        {
            string input = inputEntry.Text;
            if (string.IsNullOrEmpty(input))
            {
                await DisplayAlert("Error", "Pls write sometrhing in searching space",  "Ok");
                return;
            }
            else
            {
                string url = $"https://www.google.com/search?q={Uri.EscapeDataString(input)}" ;
                await Browser.Default.OpenAsync(url);
            }
        }

        private async void openOrganizer_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Organizer());
        }
        private async void buttonShowInfoCheck_Clicked(object sender, EventArgs e)
        {
            await Task.Delay(10000);
            await DisplayAlert("Show alert", "Allert notification", "Meow");
        }
    }
}
