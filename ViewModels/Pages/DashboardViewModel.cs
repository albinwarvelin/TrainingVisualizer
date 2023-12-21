// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using Wpf.Ui.Controls;
using WPF.Models;

namespace WPF.ViewModels.Pages;

public partial class DashboardViewModel : ObservableObject, INavigationAware
{
    private bool _isInitialized = false;
    private int temp = 0;

    [ObservableProperty]
    private int _counter = 0;

    public void OnNavigatedTo()
    {
        if (!_isInitialized)
            InitializeViewModel();
    }

    public void OnNavigatedFrom()
    {
    }

    private void InitializeViewModel()
    {
        Manager.DashboardViewModel = this;


        _isInitialized = true;

        Debug.Print("Dashboardpage initialized.");
    }

    [RelayCommand]
    private void OnCounterIncrement()
    {
        Manager.DisplayInfoBarMessage(3000, "Title", "Message", InfoBarSeverity.Informational);
        Counter++;
    }
}
