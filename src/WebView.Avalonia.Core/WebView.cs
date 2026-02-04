using System.Diagnostics.CodeAnalysis;
using Avalonia;
using Avalonia.Controls;
using Microsoft.Extensions.Logging;
using WebView.Avalonia.Core.Models;
using WebView.Avalonia.Core.Tools;
using WebViewCore.Ioc;

namespace WebView.Avalonia.Core;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
public class WebView : Control, IDisposable
{
    private static ILogger logger = LoggerFactoryTool.GetLogger<WebView>();
    
    public event EventHandler<WebViewNavigationStartingEventArgs>? NavigationStarting;

    public event EventHandler<WebViewNavigationCompletedEventArgs>? NavigationCompleted;

    #region UrlProperty
    public static readonly StyledProperty<string?> UrlProperty = AvaloniaProperty.Register<WebView, string?>(nameof(Url));

    public string? Url
    {
        get => GetValue(UrlProperty);
        set => SetValue(UrlProperty, value);
    }
    #endregion

    private WebViewDef? webViewInstance;

    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(WebViewNavigationStartingEventArgs))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(WebViewNavigationCompletedEventArgs))]
    public WebView() 
    {
        webViewInstance = WebViewLocator.ResolveInstance<WebViewDef>();

        webViewInstance?.SetControl(this);
        webViewInstance?.NavigationStarting += WebView_NavigationStarting;
        webViewInstance?.NavigationCompleted += WebView_NavigationCompleted;
    }

    private void WebView_NavigationStarting(object? sender, WebViewNavigationStartingEventArgs e)
    {
        NavigationStarting?.Invoke(sender, e);
    }

    private void WebView_NavigationCompleted(object? sender, WebViewNavigationCompletedEventArgs e)
    {
        NavigationCompleted?.Invoke(sender, e);
    }

    #region Control.Event
    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);

        if (Design.IsDesignMode)
        {
            return;
        }

        logger.LogInformation("WebView.OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)");

        webViewInstance?.OnLoadWebView();
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);

        if (Design.IsDesignMode)
        {
            return;
        }

        webViewInstance?.OnPropertyChanged(change);
    }
    #endregion

    #region OnSizeChanged
    protected override void OnSizeChanged(SizeChangedEventArgs e)
    {
        base.OnSizeChanged(e);

        if (Design.IsDesignMode)
        {
            return;
        }

        webViewInstance?.OnSizeChanged(e);
    }
    #endregion

    #region Dispose
    ~WebView()
    {
        this.Dispose();
    }

    public void Dispose()
    {
        webViewInstance?.NavigationStarting -= WebView_NavigationStarting;
        webViewInstance?.NavigationCompleted -= WebView_NavigationCompleted;

        webViewInstance?.Dispose();
    }
    #endregion
}
