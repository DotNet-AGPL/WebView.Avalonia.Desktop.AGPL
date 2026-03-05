using Avalonia;
using Microsoft.Extensions.Logging;
using WebViewCore.Ioc;

namespace WebView.Avalonia.Mac;

/// <summary>
/// macOS WebView extension for AppBuilder
/// </summary>
public static class AppBuilderExtension
{
    /// <summary>
    /// Use macOS WebView with WKWebView
    /// </summary>
    public static AppBuilder UseMacWebView(this AppBuilder builder)
    {
        WebViewLocator.RegisterSingleton<WebView.Avalonia.Core.WebViewDef, WebViewImp>();
        
        return builder;
    }
}
