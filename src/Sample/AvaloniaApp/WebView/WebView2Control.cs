using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Avalonia;
using Avalonia.Controls;
using AvaloniaApp.Tools;
using AvaloniaApp.WebView;
using Microsoft.Extensions.Logging;
using Microsoft.Web.WebView2.Core;

namespace AvaloniaApp;

// AOT异步方法保护特性
//[AotAsyncMethodPreserve(typeof(SimpleWebView2))]

public class WebView2Control : Control, IDisposable
{
    private static ILogger<WebView2Control> logger = LoggerFactoryTool.GetLoggerFactory().CreateLogger<WebView2Control>();

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

    [UnconditionalSuppressMessage("AOT", "IL3050", Justification = "WebView2 初始化已验证 AOT 兼容")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(CoreWebView2Controller))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(CoreWebView2Environment))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(CoreWebView2EnvironmentOptions))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "CoreWebView2EnvironCoreWebView2CreateCoreWebView2ControllerCompletedHandlermentOptions", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "ICoreWebView2Environment", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "ICoreWebView2Environment", "Microsoft.Web.WebView2.Core.Raw")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "ICoreWebView2EnvironmentOptions", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "ICoreWebView2EnvironmentOptions", "Microsoft.Web.WebView2.Core.Raw")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "ICoreWebView2CreateCoreWebView2EnvironmentCompletedHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "ICoreWebView2CreateCoreWebView2EnvironmentCompletedHandler", "Microsoft.Web.WebView2.Core.Raw")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(System.Runtime.InteropServices.MarshalAsAttribute))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(System.Runtime.InteropServices.InAttribute))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(System.Runtime.InteropServices.OutAttribute))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(System.Runtime.InteropServices.UnmanagedType))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(System.Runtime.InteropServices.CharSet))]
    static WebView2Control(){
        logger.LogInformation("static WebView2Control()");
    }

    #region 生命周期事件
    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        logger.LogInformation("WebView2Control.OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)");

        base.OnAttachedToVisualTree(e);
        // 仅在Windows平台执行初始化
        if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows))
        {
            logger.LogInformation("AotAsyncTaskHelper.RunSafeAsync(InitializeWebView2, continueOnCapturedContext: true)");

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
    private async Task InitializeWebView2()
    {
        logger.LogInformation("InitializeWebView2()");

        if (_isInitialized || _isDisposed || !System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows))
        {
            logger.LogInformation($"(_isInitialized || _isDisposed || !RuntimeInformation.IsOSPlatform(OSPlatform.Windows)), return");

            return;
        }
        
        var type = Type.GetType("System.StubHelpers.InterfaceMarshaler");

        logger.LogInformation($"Type.GetType(\"System.StubHelpers.InterfaceMarshaler.Methods\") = {type?.FullName}");

        var abc = type?.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static);

        var methods = type?.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static).Select(m => m.Name).ToArray();

        logger.LogInformation($"Type.GetType(\"System.StubHelpers.InterfaceMarshaler\") = {string.Join("、", methods ?? new string[0])}");

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

            new List<string>
            {
                "System.ComponentModel.TypeDescriptor.IsComObjectDescriptorSupported",
                "System.Runtime.CompilerServices.RuntimeFeature.IsDynamicCodeSupported",
                "System.Runtime.InteropServices.BuiltInComInterop.IsSupported",
                "System.Runtime.InteropServices.Marshalling.EnableGeneratedComInterfaceComImportInterop",
            }.ForEach(runtimeHost=> {
                AppContext.TryGetSwitch(runtimeHost, out bool temp);
                logger.LogInformation($"{runtimeHost}：{temp}");
            });

            //Directory.GetFiles(AppContext.BaseDirectory, "")

            //CoreWebView2Environment.SetLoaderDllFolderPath(AppDomain.CurrentDomain.BaseDirectory);

            //CoreWebView2Environment.LoadWebView2LoaderDll();
            typeof(CoreWebView2Environment).GetMethod("LoadWebView2LoaderDll", BindingFlags.Static | BindingFlags.NonPublic)?.Invoke(null, null);
            var fieldValue = typeof(CoreWebView2Environment).GetField("webView2LoaderLoaded", BindingFlags.Static | BindingFlags.NonPublic)?.GetValue(null);

            logger.LogInformation($"CoreWebView2Environment.webView2LoaderLoaded：{fieldValue}");

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

            logger.LogInformation($"_controller.ResetWebViewSize(this), before");

            // 同步控件尺寸
            _controller.ResetWebViewSize(this);

            logger.LogInformation($"_controller.ResetWebViewSize(this), after");

            // 加载初始网址
            if (!string.IsNullOrEmpty(Url))
            {
                logger.LogInformation($"_controller.CoreWebView2.Navigate(Url): {Url}");

                _controller.CoreWebView2.Navigate(Url);
            }

            _isInitialized = true;
        }
        catch (Exception ex)
        {
            logger.LogInformation($"WebView2 初始化失败: {ex.Message},{ex.StackTrace}");

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

    private static void InitSwitch() 
    {
        var runtimeHostConfigurationOptions = new List<string>
        {
            "Microsoft.Extensions.DependencyInjection.VerifyOpenGenericServiceTrimmability",
            "System.ComponentModel.DefaultValueAttribute.IsSupported",
            "System.ComponentModel.Design.IDesignerHost.IsSupported",
            "System.ComponentModel.TypeConverter.EnableUnsafeBinaryFormatterInDesigntimeLicenseContextSerialization",
            "System.ComponentModel.TypeDescriptor.IsComObjectDescriptorSupported",
            "System.Reflection.Metadata.MetadataUpdater.IsSupported",
            "System.Resources.ResourceManager.AllowCustomResourceTypes",
            "System.Resources.UseSystemResourceKeys",
            "System.Runtime.CompilerServices.RuntimeFeature.IsDynamicCodeSupported",
            "System.Runtime.InteropServices.BuiltInComInterop.IsSupported",
            "System.Runtime.InteropServices.EnableConsumingManagedCodeFromNativeHosting",
            "System.Runtime.InteropServices.EnableCppCLIHostActivation",
            "System.Runtime.InteropServices.Marshalling.EnableGeneratedComInterfaceComImportInterop",
            "System.Runtime.Serialization.EnableUnsafeBinaryFormatterSerialization",
            "System.StartupHookProvider.IsSupported",
            "System.Threading.Thread.EnableAutoreleasePool",
            "System.Text.Encoding.EnableUnsafeUTF7Encoding",
            "System.Text.Json.JsonSerializer.IsReflectionEnabledByDefault"
        };
        
        foreach (var runtimeHost in runtimeHostConfigurationOptions) 
        {
            if (!AppContext.TryGetSwitch(runtimeHost, out bool isEnabled) || !isEnabled)
            {
                // 强制启用COM开关（运行时兜底）
                AppContext.SetSwitch(runtimeHost, true);
            }

            AppContext.TryGetSwitch(runtimeHost, out bool temp);
            logger.LogInformation($"{runtimeHost}：{temp}");
        }
    }
}