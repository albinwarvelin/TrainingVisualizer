// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using Wpf.Ui.Controls;
using WPF.Models;
using WPF.ViewModels.Windows;
using WPF.Views.Pages;
using WPF.Views.Windows;

namespace WPF.ViewModels.Pages;
public partial class SettingsViewModel : ObservableObject, INavigationAware
{
    private bool _isInitialized = false;

    [ObservableProperty]
    private string _appVersion = String.Empty;

    [ObservableProperty]
    private Wpf.Ui.Appearance.ThemeType _currentTheme = Wpf.Ui.Appearance.ThemeType.Unknown;

    [ObservableProperty]
    private string _currentUserName = "";

    [ObservableProperty]
    private string _athleteID = "";

    [ObservableProperty]
    private bool _connectButtonEnabled = true;

    public void OnNavigatedTo()
    {
        if (!_isInitialized)
            InitializeViewModel();
        UpdateUserLabels();
    }

    public void OnNavigatedFrom()
    {
    }

    private void InitializeViewModel()
    {
        Manager.SettingsViewModel = this;

        CurrentTheme = Wpf.Ui.Appearance.Theme.GetAppTheme();
        AppVersion = $"WPF - {GetAssemblyVersion()}";

        _isInitialized = true;

        Debug.Print("Settingspage initialized.");
    }

    private string GetAssemblyVersion()
    {
        return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version?.ToString()
            ?? String.Empty;
    }

    private void UpdateUserLabels()
    {
        if(Manager.CurrentUser != null)
        {
            CurrentUserName = "Current user: " + Manager.CurrentUser.Name;
            AthleteID = "Athlete ID: " + Manager.CurrentUser.AthleteID;
        }
        else
        {
            CurrentUserName = "Current user: not logged in";
            AthleteID = "Athlete ID: ";
        }
    }

    [RelayCommand]
    private void OnChangeTheme(string parameter)
    {
        switch (parameter)
        {
            case "theme_light":
                if (CurrentTheme == Wpf.Ui.Appearance.ThemeType.Light)
                    break;

                Wpf.Ui.Appearance.Theme.Apply(Wpf.Ui.Appearance.ThemeType.Light);
                CurrentTheme = Wpf.Ui.Appearance.ThemeType.Light;

                break;

            default:
                if (CurrentTheme == Wpf.Ui.Appearance.ThemeType.Dark)
                    break;

                Wpf.Ui.Appearance.Theme.Apply(Wpf.Ui.Appearance.ThemeType.Dark);
                CurrentTheme = Wpf.Ui.Appearance.ThemeType.Dark;

                break;
        }
    }

    [RelayCommand]
    private void ConnectButton(string parameter)
    {
        Manager.ToggleWebview(true);

        Manager.NavigateTo("webviewPage");

        Manager.ToggleConnectButton(false);

        //After navigation to allow page to initialize
        Manager.InitializeAuthorization();
    }
}
