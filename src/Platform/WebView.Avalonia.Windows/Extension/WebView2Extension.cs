using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Avalonia.Controls;
using Microsoft.Extensions.Logging;
using WebView.Avalonia.Windows.WebView2;

namespace WebView.Avalonia.Windows.Extension;

internal static class WebView2Extension
{
    internal static bool IsWindowsJIT()
    {
        return System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows) && RuntimeFeature.IsDynamicCodeCompiled;
    }

    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(System.Runtime.InteropServices.MarshalAsAttribute))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(System.Runtime.InteropServices.InAttribute))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(System.Runtime.InteropServices.OutAttribute))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(System.Runtime.InteropServices.UnmanagedType))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(System.Runtime.InteropServices.CharSet))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2Controller))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2Environment))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2EnvironmentOptions))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.CoreWebView2EnvironCoreWebView2CreateCoreWebView2ControllerCompletedHandlermentOptions", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2Environment", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2EnvironmentOptions", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2CreateCoreWebView2EnvironmentCompletedHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2CreateCoreWebView2EnvironmentCompletedHandler", "Microsoft.Web.WebView2.Core")]
    internal static Type RegisterDependencyType(this Type obj)
    {
        Console.WriteLine("register the dependency type of webview ");

        return obj;
    }

    internal static Type SetLoaderDllFolderPath(this Type obj)
    {
        var winArch = System.Runtime.InteropServices.RuntimeInformation.ProcessArchitecture switch
        {
            System.Runtime.InteropServices.Architecture.X86 => "win-x86",
            System.Runtime.InteropServices.Architecture.X64 => "win-x64",
            System.Runtime.InteropServices.Architecture.Arm64 => "win-arm64",
            _ => ""
        };

        if (!string.IsNullOrEmpty(winArch))
        {
            var loaderDllFolderPath = Path.Combine(AppContext.BaseDirectory, "runtimes", winArch, "native");

            Microsoft.Web.WebView2.Core.CoreWebView2Environment.SetLoaderDllFolderPath(loaderDllFolderPath);
        }

        return obj;
    }


    internal static void ResetWebViewSize(this Microsoft.Web.WebView2.Core.CoreWebView2Controller coreWebView2Controller, Control? control)
    {
        if (coreWebView2Controller is null || control is null)
        {
            return;
        }

        var scale = TopLevel.GetTopLevel(control)?.RenderScaling ?? 1;

        coreWebView2Controller.Bounds = new System.Drawing.Rectangle(0, 0, Convert.ToInt32(control.Bounds.Width * scale), Convert.ToInt32(control.Bounds.Height * scale));
        coreWebView2Controller.NotifyParentWindowPositionChanged();

        return;
    }

    internal static Type InitSwitch(this Type obj, ILogger<WebView2Control>? logger = default)
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
            logger?.LogInformation($"{runtimeHost}：{temp}");
        }

        return obj;
    }
}
