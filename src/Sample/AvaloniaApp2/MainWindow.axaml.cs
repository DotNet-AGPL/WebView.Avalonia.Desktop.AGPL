using Avalonia.Controls;
using Microsoft.Extensions.Logging;
using WebView.Avalonia.Core.Tools;

namespace AvaloniaApp2
{
    public partial class MainWindow : Window
    {
        private static ILogger logger = LoggerFactoryTool.GetLogger<MainWindow>();

        public MainWindow()
        {
            InitializeComponent();

            webview.NavigationStarting += Webview2_NavigationStarting;

            webview.Url = "https://docs.avaloniaui.net/zh-Hans/docs/welcome";
        }

        private void Webview2_NavigationStarting(object? sender, WebView.Avalonia.Core.Models.WebViewNavigationStartingEventArgs e)
        {
            e.Cancel = false;

            logger.LogInformation("MainWindow.Webview2_NavigationStarting");
        }
    }
}