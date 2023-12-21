// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using System.Collections.ObjectModel;
using System.Timers;
using System.Windows;
using System.Windows.Media.Animation;
using Wpf.Ui.Common;
using Wpf.Ui.Controls;
using WPF.Models;
using Wpf.Ui.Animations;
using System.Windows.Automation.Text;

namespace WPF.ViewModels.Windows;
public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty]
    private string _applicationTitle = "WPF UI - WPF";

    [ObservableProperty]
    private ObservableCollection<object> _menuItems = new()
    {
        new NavigationViewItem()
        {
            Content = "Home",
            Icon = new SymbolIcon { Symbol = SymbolRegular.Home24 },
            TargetPageTag = "homePage",
            TargetPageType = typeof(Views.Pages.DashboardPage)
        },
        new NavigationViewItem()
        {
            Content = "Data",
            Icon = new SymbolIcon { Symbol = SymbolRegular.DataHistogram24 },
            TargetPageTag = "dataPage",
            TargetPageType = typeof(Views.Pages.DataPage)
        },
        new NavigationViewItem()
        {
            Visibility = Visibility.Collapsed,
            Content = "Webview",
            Icon = new SymbolIcon { Symbol = SymbolRegular.Globe32},
            TargetPageTag = "webviewPage",
            TargetPageType = typeof(Views.Pages.WebviewPage)
        }
    };

    [ObservableProperty]
    private ObservableCollection<object> _footerMenuItems = new()
    {
        new InfoBar()
        {
            Name = "infoBar",
            Title = "InfoBar",
            IsOpen = false,
            IsClosable = false,
            
            
        },
        new NavigationViewItem()
        {
            Content = "Settings",
            Icon = new SymbolIcon { Symbol = SymbolRegular.Settings24 },
            TargetPageType = typeof(Views.Pages.SettingsPage)
        }
    };

    [ObservableProperty]
    private ObservableCollection<MenuItem> _trayMenuItems = new()
    {
        new MenuItem { Header = "Home", Tag = "tray_home" }
    };

    private Storyboard storyboard;

    public void ToggleWebview(bool open)
    {
        if (open)
        {
            ((NavigationViewItem)MenuItems[2]).Visibility = Visibility.Visible;
        }
        else
        {
            ((NavigationViewItem)MenuItems[2]).Visibility = Visibility.Collapsed;
        }
    }

    /// <summary>
    /// Shows infobar with message.
    /// </summary>
    /// <param name="title"></param>
    /// <param name="message"></param>
    /// <param name="severity"></param>
    public void ShowInfoBar(string title, string message, InfoBarSeverity severity)
    {
        if (storyboard != null)  //Stops previous fadeout animation
        {
            storyboard.Stop();
        }

        ((InfoBar)FooterMenuItems[0]).Title = title;
        ((InfoBar)FooterMenuItems[0]).Message = message;
        ((InfoBar)FooterMenuItems[0]).Severity = severity;

        ((InfoBar)FooterMenuItems[0]).IsOpen = true;
        ((InfoBar)FooterMenuItems[0]).Opacity = 1.0;

    }

    /// <summary>
    /// Hides infobar.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    public void HideInfoBar(object source, ElapsedEventArgs e) //With fade out
    {
        Application.Current.Dispatcher.Invoke(delegate
        {
            DoubleAnimation animation = new DoubleAnimation
            {
                From = 1.0,
                To = 0.0,
                Duration = new Duration(TimeSpan.FromMilliseconds(1000))
            };

            storyboard = new Storyboard();

            storyboard.Children.Add(animation);

            Storyboard.SetTarget(animation, ((InfoBar)FooterMenuItems[0]));
            Storyboard.SetTargetProperty(animation, new PropertyPath("Opacity", 1));
            storyboard.Completed += delegate
            {
                ((InfoBar)FooterMenuItems[0]).IsOpen = false;
                storyboard.Stop();
            };

            storyboard.Begin();
        });
    }
}
