using System.Diagnostics.CodeAnalysis;
using Avalonia;
using Avalonia.Controls;
using WebView.Avalonia.Core.Models;

namespace WebView.Avalonia.Core;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
public abstract class WebViewDef : IDisposable
{
    public virtual event EventHandler<WebViewNavigationStartingEventArgs>? NavigationStarting;

    public virtual event EventHandler<WebViewNavigationCompletedEventArgs>? NavigationCompleted;

    private protected WebView? webviewInstance;

    internal void SetControl(WebView webviewInstance) 
    {
        this.webviewInstance = webviewInstance;
    }

    internal protected WebView? WebViewInstance => webviewInstance;

    public virtual void Dispose() { }

    public virtual void Reload() { }

    internal protected virtual void OnLoadWebView() { }

    internal protected virtual void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change) { }

    internal protected virtual void OnSizeChanged(SizeChangedEventArgs e) { }
}
