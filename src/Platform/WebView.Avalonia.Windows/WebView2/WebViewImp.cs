using System.Diagnostics.CodeAnalysis;
using Avalonia;
using Avalonia.Controls;
using Microsoft.Extensions.Logging;
using Microsoft.Web.WebView2.Core;
using WebView.Avalonia.Core.Models;
using WebView.Avalonia.Core.Tools;
using WebView.Avalonia.Windows.Extension;
using WebView.Avalonia.Windows.Models;
using WebView.Avalonia.Windows.Tools;

namespace WebView.Avalonia.Windows.WebView2;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
internal class WebViewImp : WebView.Avalonia.Core.WebViewDef, IDisposable
{
    private static ILogger logger = LoggerFactoryTool.GetLogger<WebViewImp>();

    internal protected override event EventHandler<WebViewNavigationStartingEventArgs>? NavigationStarting;

    internal protected override event EventHandler<WebViewNavigationCompletedEventArgs>? NavigationCompleted;

    #region Field
    private CoreWebView2Controller? _controller;
    private bool _isInitialized;
    private bool _isDisposed;
    #endregion
    
    static WebViewImp(){
        if (Design.IsDesignMode) 
        {
            return;
        }
        
        typeof(WebViewImp).RegisterWebView2DependencyType().SetLoaderDllFolderPath();

        logger.LogInformation("static WebView2Control()");
    }

    #region Init
    internal protected override void OnLoadWebView()
    {
        // 仅在Windows-JIT执行初始化
        if (WebView2Extension.IsWindowsJIT())
        {
            logger.LogInformation("AotAsyncTaskHelper.RunSafeAsync(LoadWebView2, continueOnCapturedContext: true)");

            AotAsyncTaskTool.RunSafeAsync(LoadWebView2, this.WebViewInstance, continueOnCapturedContext: true);
        }
    }

    //[UnconditionalSuppressMessage("AOT", "IL3050", Justification = "WebView2 初始化已验证 AOT 兼容")]
    private async Task LoadWebView2(Control? obj)
    {
        logger.LogInformation("InitializeWebView2()");

        if (_isInitialized || _isDisposed || !WebView2Extension.IsWindowsJIT())
        {
            logger.LogInformation($"(_isInitialized || _isDisposed || !WebView2Extension.IsWindowsJIT()");

            return;
        }

        try
        {
            logger.LogInformation("InitializeWebView2：TopLevel.GetTopLevel(this)");
            // 1. 获取控件所在的顶级窗口
            var topLevel = TopLevel.GetTopLevel(obj);
            if (topLevel == null)
            {
                logger.LogInformation("InitializeWebView2：控件未挂载到顶级窗口，无法获取句柄");
                return;
            }

            logger.LogInformation("InitializeWebView2：topLevel?.TryGetPlatformHandle()?.Handle");
            // 获取Windows原生HWND句柄（公开API，无访问级别问题）
            IntPtr hwnd = topLevel?.TryGetPlatformHandle()?.Handle ?? IntPtr.Zero;
            if (hwnd == IntPtr.Zero)
            {
                logger.LogInformation("InitializeWebView2：获取Windows原生窗口句柄失败");
                return;
            }

            logger.LogInformation("InitializeWebView2：CoreWebView2Environment.CreateAsync");
            // 创建WebView2环境
            var userDataFolder = Path.Combine(AppContext.BaseDirectory, "webview_data");
            var options = new CoreWebView2EnvironmentOptions();
            var environment = await CoreWebView2Environment.CreateAsync(browserExecutableFolder: default, userDataFolder: userDataFolder, options: options);

            logger.LogInformation("InitializeWebView2：_environment.CreateCoreWebView2ControllerAsync");
            // 创建CoreWebView2Controller并绑定句柄
            _controller = await environment.CreateCoreWebView2ControllerAsync(hwnd);
            if (_controller?.CoreWebView2 == null)
            {
                throw new NullReferenceException("CoreWebView2实例创建失败");
            }

            logger.LogInformation("InitializeWebView2： _controller.CoreWebView2.Settings.IsScriptEnabled");

            // 配置WebView2基础设置
            _controller.CoreWebView2.Settings.IsScriptEnabled = true;

            // 同步控件尺寸
            _controller.ResetWebViewSize(obj);

            _controller.CoreWebView2.NavigationStarting += CoreWebView2_NavigationStarting;
            _controller.CoreWebView2.NavigationCompleted += CoreWebView2_NavigationCompleted;

            _isInitialized = true;

            var url = this.WebViewInstance?.Url;

            // 加载初始网址
            if (!string.IsNullOrEmpty(url))
            {
                logger.LogInformation($"_controller.CoreWebView2.Navigate(Url): {url}");

                _controller.CoreWebView2.Navigate(url);
            }
            
        }
        catch (Exception ex)
        {
            logger.LogInformation($"WebView2 初始化失败: {ex.Message},{ex.StackTrace}");

            _isInitialized = false;
        }
    }

    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(WebViewNavigationStartingEventArgs))]
    private void CoreWebView2_NavigationStarting(object? sender, CoreWebView2NavigationStartingEventArgs e)
    {
        logger.LogInformation("NavigationStartingAction： " + e.ToString());

        NavigationStarting?.Invoke(this, new WebView2NavigationStartingEventArgs(e));
    }

    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(CoreWebView2NavigationCompletedEventArgs))]
    private void CoreWebView2_NavigationCompleted(object? sender, CoreWebView2NavigationCompletedEventArgs e)
    {
        logger.LogInformation("NavigationCompleted： " + e.ToString());

        NavigationCompleted?.Invoke(this, new WebView2NavigationCompletedEventArgs(e));
    }
    #endregion

    #region Event
    internal protected override void OnSizeChanged(SizeChangedEventArgs e)
    {
        if (Design.IsDesignMode)
        {
            return;
        }

        if (_controller != null && _isInitialized)
        {
            _controller.ResetWebViewSize(this.WebViewInstance);
        }
    }

    internal protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);

        if (Design.IsDesignMode)
        {
            return;
        }

        if (!_isInitialized)
        {
            return;
        }

        if (change.Property == WebView.Avalonia.Core.WebView.UrlProperty)
        {
            _controller?.CoreWebView2?.Navigate(change.NewValue as string);
        }
    }
    #endregion

    #region Function
    public override void Reload() 
    {
        _controller?.CoreWebView2?.Reload();
    }
    #endregion

    #region Dispose
    ~WebViewImp()
    {
        this.Dispose();
    }

    public override void Dispose()
    {
        if (_isDisposed) return;

        _controller?.CoreWebView2.NavigationStarting -= CoreWebView2_NavigationStarting;
        _controller?.CoreWebView2.NavigationCompleted -= CoreWebView2_NavigationCompleted;

        _controller?.Close();
        _controller = null;
        _isDisposed = true;
    }
    #endregion
}