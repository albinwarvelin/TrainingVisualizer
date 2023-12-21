using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui.Controls;
using WPF.Models;

namespace WPF.ViewModels.Pages;

public partial class WebviewViewModel : ObservableObject, INavigationAware, INotifyPropertyChanged
{
    private bool _isInitialized = false;

    [ObservableProperty]
    private string _webViewURL = null;   
    
    public void OnNavigatedFrom()
    {
        Manager.SetPageFill(true);
    }

    public void OnNavigatedTo()
    {
        if (!_isInitialized)
            InitializeViewModel();

        Manager.SetPageFill(false);
    }

    private void InitializeViewModel()
    {
        Manager.WebviewViewModel = this;
        _isInitialized = true;

        Debug.Print("Webviewpage initialized.");
    }
}
