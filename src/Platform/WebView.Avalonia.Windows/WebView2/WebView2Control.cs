using System.Diagnostics.CodeAnalysis;
using Avalonia;
using Avalonia.Controls;
using Microsoft.Extensions.Logging;
using Microsoft.Web.WebView2.Core;
using WebView.Avalonia.Core.Models;
using WebView.Avalonia.Windows.EventArg;
using WebView.Avalonia.Windows.Tools;

namespace WebView.Avalonia.Windows.WebView2;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
public class WebView2Control : WebView.Avalonia.Core.WebView, IDisposable
{
    private static ILogger<WebView2Control> logger = LoggerFactoryTool.GetLoggerFactory().CreateLogger<WebView2Control>();
    
    public override event EventHandler<NavigationStartingEventArgs>? NavigationStarting;

    #region UrlProperty
    internal static readonly StyledProperty<string?> UrlProperty = AvaloniaProperty.Register<WebView2Control, string?>(nameof(Url));

    public string? Url
    {
        get => GetValue(UrlProperty);
        set => SetValue(UrlProperty, value);
    }
    #endregion

    #region Field
    private CoreWebView2Controller? _controller;
    //private CoreWebView2Environment? _environment;
    private bool _isInitialized;
    private bool _isDisposed;
    #endregion
    
    static WebView2Control(){
        if (Design.IsDesignMode) 
        {
            return;
        }

        typeof(WebView2Control).RegisterDependencyType().SetLoaderDllFolderPath();

        logger.LogInformation("static WebView2Control()");
    }

    #region Event
    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);

        if (Design.IsDesignMode)
        {
            return;
        }

        logger.LogInformation("WebView2Control.OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)");

        // 仅在Windows-JIT执行初始化
        if (WebView2Extension.IsWindowsJIT())
        {
            logger.LogInformation("AotAsyncTaskHelper.RunSafeAsync(LoadWebView2, continueOnCapturedContext: true)");

            AotAsyncTaskTool.RunSafeAsync(LoadWebView2, continueOnCapturedContext: true);
        }
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);

        if (Design.IsDesignMode)
        {
            return;
        }

        if (!IsInitialized) 
        {
            return;
        }


        if (change.Property == UrlProperty)
        {
            _controller?.CoreWebView2?.Navigate(Url);
        }
    }

    
    #endregion

    #region Init
    //[UnconditionalSuppressMessage("AOT", "IL3050", Justification = "WebView2 初始化已验证 AOT 兼容")]
    private async Task LoadWebView2()
    {
        logger.LogInformation("InitializeWebView2()");

        if (_isInitialized || _isDisposed || !WebView2Extension.IsWindowsJIT())
        {
            logger.LogInformation($"(_isInitialized || _isDisposed || !WebView2Extension.IsWindowsJIT()");

            return;
        }

        try
        {
            logger.LogError("InitializeWebView2：TopLevel.GetTopLevel(this)");
            // 1. 获取控件所在的顶级窗口
            var topLevel = TopLevel.GetTopLevel(this);
            if (topLevel == null)
            {
                logger.LogError("InitializeWebView2：控件未挂载到顶级窗口，无法获取句柄");
                return;
            }

            logger.LogError("InitializeWebView2：topLevel?.TryGetPlatformHandle()?.Handle");
            // 获取Windows原生HWND句柄（公开API，无访问级别问题）
            IntPtr hwnd = topLevel?.TryGetPlatformHandle()?.Handle ?? IntPtr.Zero;
            if (hwnd == IntPtr.Zero)
            {
                logger.LogError("InitializeWebView2：获取Windows原生窗口句柄失败");
                return;
            }

            logger.LogError("InitializeWebView2：CoreWebView2Environment.CreateAsync");
            // 创建WebView2环境
            var userDataFolder = Path.Combine(AppContext.BaseDirectory, "webview_data");
            var options = new CoreWebView2EnvironmentOptions();
            var environment = await CoreWebView2Environment.CreateAsync(browserExecutableFolder: default, userDataFolder: userDataFolder, options: options);

            logger.LogError("InitializeWebView2：_environment.CreateCoreWebView2ControllerAsync");
            // 创建CoreWebView2Controller并绑定句柄
            _controller = await environment.CreateCoreWebView2ControllerAsync(hwnd);
            if (_controller?.CoreWebView2 == null)
            {
                throw new NullReferenceException("CoreWebView2实例创建失败");
            }

            logger.LogError("InitializeWebView2： _controller.CoreWebView2.Settings.IsScriptEnabled");

            // 配置WebView2基础设置
            _controller.CoreWebView2.Settings.IsScriptEnabled = true;

            // 同步控件尺寸
            _controller.ResetWebViewSize(this);

            _controller.CoreWebView2.NavigationStarting += NavigationStartingAction;

            // 加载初始网址
            if (!string.IsNullOrEmpty(Url))
            {
                logger.LogInformation($"_controller.CoreWebView2.Navigate(Url): {Url}");

                _controller.CoreWebView2.Navigate(Url);
            }
        }
        catch (Exception ex)
        {
            logger.LogInformation($"WebView2 初始化失败: {ex.Message},{ex.StackTrace}");

            _isInitialized = false;
        }

        _isInitialized = true;
    }

    private void NavigationStartingAction(object? sender, CoreWebView2NavigationStartingEventArgs e)
    {
        //e.NavigationKind

        logger.LogError("NavigationStartingAction： " + e.ToString());

        NavigationStarting?.Invoke(this, new WebView2NavigationStartingEventArgs(e));
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

        if (_controller != null && _isInitialized)
        {
            _controller.ResetWebViewSize(this);
        }
    }
    #endregion

    #region Dispose
    ~WebView2Control()
    {
        this.Dispose();
    }

    public override void Dispose()
    {
        if (_isDisposed) return;

        _controller?.Close();//.Dispose();
        //_environment = null;
        _controller = null;
        _isDisposed = true;
    }
    #endregion
}