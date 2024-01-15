using Avalonia.Controls;
using Avalonia.Threading;
using MyWeather.Class;
using System.Collections.Generic;
using System.Linq;

namespace MyWeather.Views;

public partial class MainWindow : Window
{
    MainPageWindows MainPageWindows;
    SettingsPage settingsPage;
    LovePage lovePage;
    SearchPage searchPage;
    MainView mainView;

    public MainWindow()
    {
        InitializeComponent();

        mainView = new MainView();
        MainBorder.Child = mainView;

        Save.InitFilesForSave();

        Save.Settings = Save.LoadData<Settings>(Save.PathFileSettings);
        Save.Loves = Save.LoadData<List<Love>>(Save.PathFileLove);

        LoadMainPageWindows();

        this.PointerPressed += MainWindow_PointerPressed;
    }

    private async void LoadMainPageWindows()
    {
        string City = await IPAPI.GetTheCityByIP();
        string lat = "", lon = "";
        (lat, lon) = await IPAPI.GetTheLatAndLogByIP();

        bool isF = false;
        if (Save.Loves.Where(l => l.Name == City).ToList().Count != 0)
            isF = true;

        WeatherContent weatherContent = await WeatherAPI.GetWeather(lat, lon);
        ShowMainPage(new WeatherCity(City, isF), weatherContent);
    }

    private void MainWindow_PointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e)
    {
        this.BeginMoveDrag(e);
    }

    public void ShowMainPage(WeatherCity weatherCity, WeatherContent weatherContent)
    {
        MainPageWindows = new MainPageWindows(weatherCity, weatherContent);
        MainPageWindows.ClickLoveBtn += MainPageWindows_ClickLoveBtn;
        MainPageWindows.ClickSettingsBtn += MainPageWindows_ClickSettingsBtn;
        MainPageWindows.ClickSearchBtn += MainPageWindows_ClickSearchBtn;
        MainBorder.Child = MainPageWindows;
    }

    private void MainPageWindows_ClickSearchBtn(string searchText)
    {
        MainBorder.Child = mainView;
        searchPage = new SearchPage(searchText);
        searchPage.LoadedData += SearchPage_LoadedData;
    }

    private void MainPageWindows_ClickSettingsBtn()
    {
        settingsPage = new SettingsPage();
        settingsPage.ClickBackBtn += SettingsPage_ClickBackBtn;
        MainBorder.Child = settingsPage;
    }

    private void MainPageWindows_ClickLoveBtn()
    {
        MainBorder.Child = mainView;
        lovePage = new LovePage();
        lovePage.LoadedData += LovePage_LoadedData;
    }

    private void SearchPage_LoadedData()
    {
        searchPage.LoadedData -= SearchPage_LoadedData;
        searchPage.ClickBackBtn += SearchPage_ClickBackBtn;
        searchPage.SelectNode += SearchPage_SelectNode;
        searchPage.ClickSearchBtn += MainPageWindows_ClickSearchBtn;
        MainBorder.Child = searchPage;
    }

    private void SearchPage_SelectNode(WeatherContent content, string city)
    {
        MainPageWindows.ClickLoveBtn -= MainPageWindows_ClickLoveBtn;
        MainPageWindows.ClickSettingsBtn -= MainPageWindows_ClickSettingsBtn;
        MainPageWindows.ClickSearchBtn -= MainPageWindows_ClickSearchBtn;
        searchPage.SelectNode -= SearchPage_SelectNode;
        searchPage.ClickBackBtn -= SearchPage_ClickBackBtn;
        searchPage.ClickSearchBtn -= MainPageWindows_ClickSearchBtn;

        bool isF = false;
        if (Save.Loves.Where(l => l.Name == city).ToList().Count != 0)
            isF = true;

        ShowMainPage(new WeatherCity(city, isF), content);
        MainBorder.Child = MainPageWindows;
    }

    private void SearchPage_ClickBackBtn()
    {
        searchPage.LoadedData -= SearchPage_LoadedData;
        searchPage.SelectNode -= SearchPage_SelectNode;
        searchPage.ClickBackBtn -= SearchPage_ClickBackBtn;
        searchPage.ClickSearchBtn -= MainPageWindows_ClickSearchBtn;
        MainBorder.Child = MainPageWindows;
    }

    private void LovePage_LoadedData()
    {
        lovePage.LoadedData -= LovePage_LoadedData;
        lovePage.ClickBackBtn += LovePage_ClickBackBtn;
        lovePage.SelectNode += LovePage_SelectNode;
        MainBorder.Child = lovePage;
    }

    private void LovePage_SelectNode(WeatherContent content, string city)
    {
        MainPageWindows.ClickLoveBtn -= MainPageWindows_ClickLoveBtn;
        MainPageWindows.ClickSettingsBtn -= MainPageWindows_ClickSettingsBtn;
        MainPageWindows.ClickSearchBtn -= MainPageWindows_ClickSearchBtn;
        lovePage.SelectNode -= LovePage_SelectNode;
        lovePage.ClickBackBtn -= LovePage_ClickBackBtn;
        ShowMainPage(new WeatherCity(city, true), content);
        MainBorder.Child = MainPageWindows;
    }

    private void LovePage_ClickBackBtn()
    {
        lovePage.LoadedData -= LovePage_LoadedData;
        lovePage.SelectNode -= LovePage_SelectNode;
        lovePage.ClickBackBtn -= LovePage_ClickBackBtn;
        MainBorder.Child = MainPageWindows;
    }

    private void SettingsPage_ClickBackBtn()
    {
        settingsPage.ClickBackBtn -= SettingsPage_ClickBackBtn;
        MainBorder.Child = MainPageWindows;
    }
}
