using System.Diagnostics.CodeAnalysis;
using Avalonia;
using Avalonia.Controls;
using Microsoft.Extensions.Logging;
using WebKit;
using WebView.Avalonia.Core;
using WebView.Avalonia.Core.Models;
using WebView.Avalonia.Core.Tools;
using WebView.Avalonia.Linux.Models;

namespace WebView.Avalonia.Linux;

/// <summary>
/// Linux WebView implementation using WebKit
/// </summary>
[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
internal class WebViewImp : WebView.Avalonia.Core.WebViewDef, IDisposable
{
    private static ILogger logger = LoggerFactoryTool.GetLogger<WebViewImp>();

    internal protected override event EventHandler<WebViewNavigationStartingEventArgs>? NavigationStarting;

    internal protected override event EventHandler<WebViewNavigationCompletedEventArgs>? NavigationCompleted;

    #region Field
    private WebKit.WebView? _webView;
    private bool _isInitialized;
    private bool _isDisposed;
    private ulong _navigationId;
    #endregion

    static WebViewImp()
    {
        if (Design.IsDesignMode)
        {
            return;
        }

        logger.LogInformation("WebViewImp static constructor");
    }

    #region Init

    /// <summary>
    /// Initialize the WebView on Linux
    /// </summary>
    internal protected override void OnLoadWebView()
    {
        // Linux 环境初始化
        if (IsLinuxRuntime())
        {
            logger.LogInformation("Linux WebView: Starting initialization");

            try
            {
                // 尝试初始化 WebKit
                InitializeWebKit();
            }
            catch (Exception ex)
            {
                logger.LogInformation($"Linux WebView: Initialization failed - {ex.Message}");
            }
        }
    }

    private bool IsLinuxRuntime()
    {
        return System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Linux);
    }

    /// <summary>
    /// Initialize WebKit WebView
    /// </summary>
    private void InitializeWebKit()
    {
        if (_isInitialized || _isDisposed)
        {
            return;
        }

        logger.LogInformation("Linux WebView: Initializing WebKit...");

        // Note: Actual WebKit integration requires:
        // 1. WebKitGTK native library (libwebkit2gtk-4.1.so)
        // 2. P/Invoke bindings or a .NET binding like WebkitGtkSharp
        //
        // This is a placeholder implementation that demonstrates the architecture.
        // To complete the implementation:
        // - Add WebkitGtkSharp NuGet package
        // - Use P/Invoke to call WebKitGTK APIs
        // - Or use the Avalonia Accelerate WebView component

        _webView = new WebKit.WebView();

        _webView.DecidePolicy += _webView_DecidePolicy;

        _webView.LoadChanged += WebKitWebView_LoadChanged;

        _isInitialized = true;
        
        logger.LogInformation("Linux WebView: WebKit placeholder initialized");

        // 加载初始 URL
        var url = this.WebViewInstance?.Url;
        if (!string.IsNullOrEmpty(url))
        {
            NavigateToUrl(url);
        }
    }

    private void _webView_DecidePolicy(object o, DecidePolicyArgs args)
    {
        //args.
        throw new NotImplementedException();
    }

    private void WebKitWebView_LoadChanged(object? sender, LoadChangedArgs e)
    {
        logger.LogInformation("WebKitWebView_LoadChangedAction： " + e.ToString());
        
        switch (e.LoadEvent)
        {
            default:
                break;
            case LoadEvent.Started:

                Console.WriteLine("开始加载网页");
                NavigationStarting?.Invoke(sender, new WebKitWebViewNavigationStartingEventArgs(sender as WebKit.WebView, e));
                break;
            case LoadEvent.Finished:
                Console.WriteLine("网页加载完成");
                NavigationCompleted?.Invoke(sender, new WebKitWebViewNavigationCompletedEventArgs(e));
                break;
        }
    }



    /// <summary>
    /// Navigate to a URL
    /// </summary>
    private void NavigateToUrl(string url)
    {
        if (string.IsNullOrEmpty(url))
        {
            return;
        }

        _navigationId++;
        
        var args = new LinuxWebViewNavigationStartingEventArgs(
            uri: url,
            navigationId: _navigationId
        );

        // 触发导航开始事件
        NavigationStarting?.Invoke(this, args);

        logger.LogInformation($"Linux WebView: Navigating to {url}");
    }

    #endregion

    #region Event Handlers

    internal protected override void OnSizeChanged(SizeChangedEventArgs e)
    {
        if (Design.IsDesignMode)
        {
            return;
        }

        // 更新 WebView 尺寸
        logger.LogInformation("Linux WebView: Size changed");
    }

    internal protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);

        if (Design.IsDesignMode)
        {
            return;
        }

        if (change.Property == WebView.Avalonia.Core.WebView.UrlProperty)
        {
            var url = change.NewValue as string;
            if (!string.IsNullOrEmpty(url))
            {
                NavigateToUrl(url);
            }
        }
    }

    #endregion

    #region Public Methods

    public override void Reload()
    {
        logger.LogInformation("Linux WebView: Reload requested");
        
        // 重新加载当前页面
        var url = this.WebViewInstance?.Url;
        if (!string.IsNullOrEmpty(url))
        {
            NavigateToUrl(url);
        }
    }

    /// <summary>
    /// Navigate to a specific URL
    /// </summary>
    public void Navigate(string url)
    {
        NavigateToUrl(url);
    }

    /// <summary>
    /// Go back in history
    /// </summary>
    public void GoBack()
    {
        logger.LogInformation("Linux WebView: GoBack requested");
    }

    /// <summary>
    /// Go forward in history
    /// </summary>
    public void GoForward()
    {
        logger.LogInformation("Linux WebView: GoForward requested");
    }

    /// <summary>
    /// Stop current navigation
    /// </summary>
    public void Stop()
    {
        logger.LogInformation("Linux WebView: Stop requested");
    }

    #endregion

    #region Dispose

    ~WebViewImp()
    {
        this.Dispose();
    }

    public override void Dispose()
    {
        if (_isDisposed)
        {
            return;
        }

        logger.LogInformation("Linux WebView: Disposing");

        _webView?.LoadChanged -= WebKitWebView_LoadChanged;

        NavigationStarting = null;
        NavigationCompleted = null;

        _isDisposed = true;
    }

    #endregion
}
