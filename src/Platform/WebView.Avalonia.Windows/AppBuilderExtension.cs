using Avalonia;
using WebView.Avalonia.Core;
using WebView.Avalonia.Windows.WebView2;
using WebViewCore.Ioc;

namespace WebView.Avalonia.Windows;

public static class AppBuilderExtension
{
    public static AppBuilder UseWebView2(this AppBuilder appBuilder) 
    {
        WebViewLocator.RegisterSingleton<WebViewDef, WebViewImp>();

        return appBuilder;
    }
}
