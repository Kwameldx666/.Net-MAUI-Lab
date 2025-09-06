namespace Family_Rewards_Bank;

public partial class PhotoPage : ContentPage
{
    public PhotoPage(string imagePath)
    {
        InitializeComponent();
        photoView.Source = ImageSource.FromFile(imagePath);
    }
}
