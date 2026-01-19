using System.Diagnostics.CodeAnalysis;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using Avalonia;
using Avalonia.Controls;
using AvaloniaApp.WebView;
using Microsoft.Web.WebView2.Core;

namespace AvaloniaApp;

// AOT异步方法保护特性
//[AotAsyncMethodPreserve(typeof(SimpleWebView2))]

public class WebView2Control : Control, IDisposable
{
    #region 依赖属性（用于绑定/设置网址）
    public static readonly StyledProperty<string?> UrlProperty = AvaloniaProperty.Register<WebView2Control, string?>(nameof(Url));

    public string? Url
    {
        get => GetValue(UrlProperty);
        set => SetValue(UrlProperty, value);
    }
    #endregion

    #region 私有字段
    private CoreWebView2Controller? _controller;
    private CoreWebView2Environment? _environment;
    private bool _isInitialized;
    private bool _isDisposed;
    #endregion

    #region 生命周期事件
    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        // 仅在Windows平台执行初始化
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            AotAsyncTaskHelper.RunSafeAsync(InitializeWebView2, continueOnCapturedContext: true);
        }
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);
        if (change.Property == UrlProperty && _isInitialized && !string.IsNullOrEmpty(Url))
        {
            _controller?.CoreWebView2?.Navigate(Url);
        }
    }
    #endregion

    #region 核心初始化逻辑（修正：Avalonia 11 正确获取Windows HWND）
    [UnconditionalSuppressMessage("AOT", "IL3050", Justification = "WebView2 初始化已验证 AOT 兼容")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(CoreWebView2Controller))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(CoreWebView2Environment))]
    private async Task InitializeWebView2()
    {
        if (_isInitialized || _isDisposed || !RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            return;

        try
        {
            // 1. 获取控件所在的顶级窗口
            var topLevel = TopLevel.GetTopLevel(this);
            if (topLevel == null)
            { 
                throw new InvalidOperationException("控件未挂载到顶级窗口，无法获取句柄");
            }

            // 获取Windows原生HWND句柄（公开API，无访问级别问题）
            IntPtr hwnd = topLevel?.TryGetPlatformHandle()?.Handle ?? IntPtr.Zero;
            if (hwnd == IntPtr.Zero) 
            {
                throw new InvalidOperationException("获取Windows原生窗口句柄失败");
            }

            // 创建WebView2环境
            _environment = await CoreWebView2Environment.CreateAsync();

            // 创建CoreWebView2Controller并绑定句柄
            _controller = await _environment.CreateCoreWebView2ControllerAsync(hwnd);
            if (_controller?.CoreWebView2 == null)
            {
                throw new NullReferenceException("CoreWebView2实例创建失败");
            }

            // 配置WebView2基础设置
            _controller.CoreWebView2.Settings.IsScriptEnabled = true;

            // 同步控件尺寸
            _controller.ResetWebViewSize(this);

            // 加载初始网址
            if (!string.IsNullOrEmpty(Url))
            {
                _controller.CoreWebView2.Navigate(Url);
            }

            _isInitialized = true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"WebView2 初始化失败: {ex.Message}");
            _isInitialized = false;
        }
    }
    #endregion

    #region 尺寸同步
    protected override void OnSizeChanged(SizeChangedEventArgs e)
    {
        base.OnSizeChanged(e);
        if (_controller != null && _isInitialized)
        {
            _controller.ResetWebViewSize(this);
        }
    }
    #endregion

    #region 资源释放
    public void Dispose()
    {
        if (_isDisposed) return;

        _controller?.Close();//.Dispose();
        _environment = null;
        _controller = null;
        _isDisposed = true;
    }
    #endregion
}