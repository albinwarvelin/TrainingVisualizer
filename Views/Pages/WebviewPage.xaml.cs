using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wpf.Ui.Controls;
using WPF.Models;
using WPF.ViewModels.Pages;

namespace WPF.Views.Pages
{
    public partial class WebviewPage : INavigableView<WebviewViewModel>
    {
        public WebviewViewModel ViewModel
        {
            get;
        }

        public WebviewPage(WebviewViewModel webviewViewModel)
        {
            ViewModel = webviewViewModel;
            DataContext = this;

            InitializeComponent();
        }

        private void WebView2_NavigationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs e)
        {
            Manager.AuthUrlChangeHandler();
        }
    }
}
