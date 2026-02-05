using Avalonia;
using WebView.Avalonia.Core;
using WebView.Avalonia.Windows.Extension;
using WebView.Avalonia.Windows.WebView2;
using WebViewCore.Ioc;

namespace WebView.Avalonia.Windows;

public static class AppBuilderExtension
{
    public static AppBuilder UseWinJitWebView2(this AppBuilder appBuilder) 
    {
        WebViewLocator.RegisterSingleton<WebViewDef, WebViewImp>();

        appBuilder.AfterSetup(app => 
        {
            app.RegisterWebView2DependencyType().SetWebView2LoaderDllFolderPath();
        });

        return appBuilder;
    }
}
