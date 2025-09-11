
using System.Collections.ObjectModel;using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Family_Rewards_Bank.Data;
using Family_Rewards_Bank.Models;
using Family_Rewards_Bank.ViewModels;


public partial class WebServiceEmulationViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<NewsModel> news = new();

    [ObservableProperty]
    private string selectedSource;

    [ObservableProperty]
    private string entryText;

    private readonly ApiNewsData _api;

    public ObservableCollection<string> AvailableSources { get; } = new()
    {
        "https://news.yam.md/ru/rss",
        "https://news.yam.md/ro/rss"
    };

    public WebServiceEmulationViewModel()
    {
        _api = new ApiNewsData();
    }

    [RelayCommand]
    private async Task LoadNews()
    {
        if (string.IsNullOrEmpty(SelectedSource))
        {
            await Shell.Current.DisplayAlert("Error", "Select a news source first!", "Ok");
            return;
        }

        var newsFromApi = await _api.GetNews(SelectedSource);

        // Чистим коллекцию и добавляем новости
        News.Clear();
        foreach (var item in newsFromApi)
            News.Add(item);
    }

    [RelayCommand]
    private async Task OpenNews(string url)
    {
        if (!string.IsNullOrEmpty(url))
            await Browser.Default.OpenAsync(url);
        else
            await Shell.Current.DisplayAlert("URL ERROR", "Incorrect address", "=_=");
    }

    [RelayCommand]
    private async Task AddAdress()
    {
        if (!string.IsNullOrEmpty(EntryText))
        {
            AvailableSources.Add(EntryText);
            SelectedSource = EntryText;
            await LoadNews();
        }
    }

    partial void OnSelectedSourceChanged(string value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            MainThread.BeginInvokeOnMainThread(async () => await LoadNews());
        }
    }
}
