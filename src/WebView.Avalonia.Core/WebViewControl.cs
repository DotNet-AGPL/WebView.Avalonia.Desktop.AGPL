using System.Diagnostics.CodeAnalysis;
using Avalonia;
using Avalonia.Controls;
using Microsoft.Extensions.Logging;
using WebView.Avalonia.Core.Models;
using WebView.Avalonia.Windows.Tools;

namespace WebView.Avalonia.Core;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
public class WebView : Control, IDisposable
{
    private static ILogger<WebView> logger = LoggerFactoryTool.GetLoggerFactory().CreateLogger<WebView>();

    public virtual event EventHandler<WebViewNavigationStartingEventArgs>? NavigationStarting;

    #region UrlProperty
    public static readonly StyledProperty<string?> UrlProperty = AvaloniaProperty.Register<WebView, string?>(nameof(Url));

    public string? Url
    {
        get => GetValue(UrlProperty);
        set => SetValue(UrlProperty, value);
    }
    #endregion

    public WebViewDef? webViewInstance;

    public WebView() 
    {
        webViewInstance = default!;

        webViewInstance?.SetControl(this);
    }

    #region Event
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
        webViewInstance?.Dispose();
    }
    #endregion
}
