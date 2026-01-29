using Avalonia.Controls;

namespace AvaloniaApp2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            webview2.NavigationStarting += Webview2_NavigationStarting;

            webview2.Url = "https://docs.avaloniaui.net/zh-Hans/docs/welcome";
        }

        private void Webview2_NavigationStarting(object? sender, WebView.Avalonia.Core.Models.WebViewNavigationStartingEventArgs e)
        {
            e.Cancel = false;
        }
    }
}