using System.Diagnostics.CodeAnalysis;
using Avalonia.Controls;
using Microsoft.Extensions.Logging;
using Microsoft.Web.WebView2.Core;
using WebView.Avalonia.Windows.WebView;

namespace WebView.Avalonia.Windows.Extension
{
    internal static class WebView2Extension
    {
        public static void ResetWebViewSize(this CoreWebView2Controller coreWebView2Controller, Control control)
        {
            if (coreWebView2Controller is null)
            {
                return;
            }

            var scale = TopLevel.GetTopLevel(control)?.RenderScaling ?? 1;

            coreWebView2Controller.Bounds = new System.Drawing.Rectangle(0, 0, Convert.ToInt32(control.Bounds.Width * scale), Convert.ToInt32(control.Bounds.Height * scale));
            coreWebView2Controller.NotifyParentWindowPositionChanged();

            return;
        }

        public static Type RegisterDependencyType(this Type obj)
        {
            obj.RegisterRuntimeInteropDependencyType();
            obj.RegisterWebViewDependencyType();

            return obj;
        }

        public static Type InitSwitch(this Type obj, ILogger<WebView2Control>? logger = default)
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

        [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(System.Runtime.InteropServices.MarshalAsAttribute))]
        [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(System.Runtime.InteropServices.InAttribute))]
        [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(System.Runtime.InteropServices.OutAttribute))]
        [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(System.Runtime.InteropServices.UnmanagedType))]
        [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(System.Runtime.InteropServices.CharSet))]
        private static Type RegisterRuntimeInteropDependencyType(this Type obj)
        {
            Console.WriteLine("register the dependency type of runtime-interop");

            return obj;
        }

        [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(CoreWebView2Controller))]
        [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(CoreWebView2Environment))]
        [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(CoreWebView2EnvironmentOptions))]
        [DynamicDependency(DynamicallyAccessedMemberTypes.All, "CoreWebView2EnvironCoreWebView2CreateCoreWebView2ControllerCompletedHandlermentOptions", "Microsoft.Web.WebView2.Core")]
        [DynamicDependency(DynamicallyAccessedMemberTypes.All, "ICoreWebView2Environment", "Microsoft.Web.WebView2.Core")]
        //[DynamicDependency(DynamicallyAccessedMemberTypes.All, "ICoreWebView2Environment", "Microsoft.Web.WebView2.Core.Raw")]
        [DynamicDependency(DynamicallyAccessedMemberTypes.All, "ICoreWebView2EnvironmentOptions", "Microsoft.Web.WebView2.Core")]
        //[DynamicDependency(DynamicallyAccessedMemberTypes.All, "ICoreWebView2EnvironmentOptions", "Microsoft.Web.WebView2.Core.Raw")]
        [DynamicDependency(DynamicallyAccessedMemberTypes.All, "ICoreWebView2CreateCoreWebView2EnvironmentCompletedHandler", "Microsoft.Web.WebView2.Core")]
        //[DynamicDependency(DynamicallyAccessedMemberTypes.All, "ICoreWebView2CreateCoreWebView2EnvironmentCompletedHandler", "Microsoft.Web.WebView2.Core.Raw")]
        private static Type RegisterWebViewDependencyType(this Type obj)
        {
            Console.WriteLine("register the dependency type of webview ");

            return obj;
        }
    }
}
