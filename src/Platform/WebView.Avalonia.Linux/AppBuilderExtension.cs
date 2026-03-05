using Avalonia;
using WebView.Avalonia.Core;
using WebViewCore.Ioc;

namespace WebView.Avalonia.Linux;

/// <summary>
/// Linux platform extension for Avalonia AppBuilder
/// </summary>
public static class AppBuilderExtension
{
    /// <summary>
    /// Use Linux WebView with WebKit
    /// </summary>
    public static AppBuilder UseLinuxWebView(this AppBuilder appBuilder)
    {
        // Register Linux WebView implementation
        WebViewLocator.RegisterSingleton<WebViewDef, WebViewImp>();

        return appBuilder;
    }
}
