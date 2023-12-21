using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using Microsoft.Web.WebView2.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Wpf.Ui.Controls;
using WPF.ViewModels.Pages;
using WPF.ViewModels.Windows;


namespace WPF.Models;
public static class Manager
{
    private static MainWindowViewModel _mainWindowViewModel;
    private static DashboardViewModel _dashboardViewModel;
    private static DataViewModel _dataViewModel;
    private static SettingsViewModel _settingsViewModel;
    private static WebviewViewModel _webviewViewModel;

    private static User _currentUser;
    private static NavigationView _navigationView;
    private static System.Timers.Timer _infoBarTimer;

    /// <summary>
    /// Sets MainWindowViewModel reference
    /// </summary>
    public static MainWindowViewModel MainWindowViewModel
    {
        set => _mainWindowViewModel = value;
    }
    /// <summary>
    /// Sets DashboardViewModel reference
    /// </summary>
    public static DashboardViewModel DashboardViewModel
    {
        set => _dashboardViewModel = value;
    }
    /// <summary>
    /// Sets DataViewModel reference
    /// </summary>
    public static DataViewModel DataViewModel
    {
        set => _dataViewModel = value;
    }
    /// <summary>
    /// Sets SettingsViewModel reference
    /// </summary>
    public static SettingsViewModel SettingsViewModel
    {
        set => _settingsViewModel = value;
    }
    /// <summary>
    /// Sets WebviewViewModel reference
    /// </summary>
    public static WebviewViewModel WebviewViewModel
    {
        set => _webviewViewModel = value;
    }

    /// <summary>
    /// Gets current user
    /// </summary>
    public static User CurrentUser => _currentUser;
    /// <summary>
    /// Sets NavigationView reference
    /// </summary>
    public static NavigationView NavigationView
    {
        set => _navigationView = value;
    }
    /// <summary>
    /// Navigates to page
    /// </summary>
    /// <param name="pageTag"></param>
    public static void NavigateTo(string pageTag)
    {
        _navigationView.Navigate(pageTag);
    }

    

    /// <summary>
    /// Reads and returns string of given key in resourcefile
    /// </summary>
    /// <param name="file"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static string GetResourceString(Type file, string key)
    {
        ResourceManager rm = new ResourceManager(file);

        return rm.GetString(key);
    }
    
    /// <summary>
    /// Set page fill visible or collapsed
    /// </summary>
    /// <param name="fill"></param>
    public static void SetPageFill(bool fill)
    {
        if (fill) //Restore
        {
            _navigationView.HeaderVisibility = Visibility.Visible;
            _navigationView.Padding = new Thickness(42, 0, 42, 0);
        }
        else //Collapse
        {

            _navigationView.HeaderVisibility = Visibility.Collapsed;
            _navigationView.Padding = new Thickness(0, 0, 0, 0);
        }
    }

    public static void ToggleWebview(bool open)
    {
        _mainWindowViewModel.ToggleWebview(open);
    }

    public static void InitializeAuthorization()
    {
        _webviewViewModel.WebViewURL = "https://www.strava.com/oauth/authorize?client_id=" + GetResourceString(typeof(Resources.stravaResources), "client_id") +
                                              "&response_type=code&redirect_uri=http://localhost&approval_prompt=force&scope=read_all,activity:read_all,profile:read_all";
        
        //TODO: Auth: 1. Listen for url updates
        //TODO: Auth: 2. On update check for faulty urls -> display error, cause and reset flow
        //TODO: Auth: 3. Store auth code
        //TODO: Auth: 4. HTTP request for access token and refresh token
        //TODO: Auth: 5. Save tokens and display message
        //TODO: Auth: 6. Update labels in settings and make request for athlete info
    }

    //Called upon url change
    public static void AuthUrlChangeHandler()
    {
        Debug.Print("New URL: " + _webviewViewModel.WebViewURL);
    }

    public static void ToggleConnectButton(bool enabled)
    {
        _settingsViewModel.ConnectButtonEnabled = enabled;
        //TODO: Add Darkened button when off.
    }

    public static void DisplayInfoBarMessage(int milliSeconds, string title, string message, InfoBarSeverity severity)
    {
        if(_infoBarTimer != null)
        {
            _infoBarTimer.Stop();
        }

        _mainWindowViewModel.ShowInfoBar(title, message, severity);

        _infoBarTimer = new System.Timers.Timer(milliSeconds);
        _infoBarTimer.Elapsed += new ElapsedEventHandler(_mainWindowViewModel.HideInfoBar);
        _infoBarTimer.AutoReset = false;
        _infoBarTimer.Start();
    }
}
 