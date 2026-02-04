using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Avalonia.Controls;

namespace WebView.Avalonia.Windows.Extension;

internal static class WebView2Extension
{
    internal static bool IsWindowsJIT()
    {
        return System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows) && RuntimeFeature.IsDynamicCodeCompiled;
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

    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2BrowserProcessExitKind))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2BrowsingDataKinds))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2CapturePreviewImageFormat))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2ClientCertificateKind))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2ContextMenuTargetKind))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2CookieSameSiteKind))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2DefaultDownloadDialogCornerAlignment))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2DownloadInterruptReason))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2DownloadState))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2FaviconImageFormat))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2FileSystemHandleKind))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2FrameKind))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2HostResourceAccessKind))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2KeyEventKind))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2MemoryUsageTargetLevel))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2MouseEventKind))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2MouseEventVirtualKeys))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2NonClientRegionKind))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2PdfToolbarItems))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2PermissionKind))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2PermissionState))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2PointerEventKind))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2PreferredColorScheme))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2PrintCollation))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2PrintColorMode))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2PrintDialogKind))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2PrintDuplex))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2PrintMediaSize))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2PrintOrientation))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2PrintStatus))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2ProcessFailedKind))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2ProcessFailedReason))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2ProcessKind))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2SaveAsKind))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2SaveAsUIResult))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2ScriptDialogKind))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2ServerCertificateErrorAction))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2SharedBufferAccess))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2TextDirectionKind))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2TrackingPreventionLevel))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2WebResourceContext))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2WebResourceRequestSourceKinds))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2Color))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2BasicAuthenticationRequestedEventArgs))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2BasicAuthenticationResponse))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2BrowserExtension))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2Certificate))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2ClientCertificate))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2ClientCertificateRequestedEventArgs))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2ContentLoadingEventArgs))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2ContextMenuRequestedEventArgs))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2ContextMenuTarget))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2Cookie))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2CookieManager))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2DOMContentLoadedEventArgs))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2Deferral))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2DevToolsProtocolEventReceivedEventArgs))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2DevToolsProtocolEventReceiver))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2DownloadOperation))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2DownloadStartingEventArgs))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2ExecuteScriptResult))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2File))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2Find))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2Frame))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2FrameCreatedEventArgs))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2FrameInfo))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2HttpResponseHeaders))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2LaunchingExternalUriSchemeEventArgs))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2NewWindowRequestedEventArgs))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2NonClientRegionChangedEventArgs))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2Notification))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2NotificationReceivedEventArgs))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2PermissionRequestedEventArgs))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2PermissionSetting))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2PrivateHostObjectAsyncMethodContinuation", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2PrivateRemoteObjectProxy", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2ProcessFailedEventArgs))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2Profile))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2RestartRequestedEventArgs))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2SaveAsUIShowingEventArgs))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2SaveFileSecurityCheckStartingEventArgs))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2ScreenCaptureStartingEventArgs))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2ScriptDialogOpeningEventArgs))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2ScriptException))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2ServerCertificateErrorDetectedEventArgs))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2SourceChangedEventArgs))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2WebMessageReceivedEventArgs))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2WebResourceRequestedEventArgs))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2WebResourceResponseReceivedEventArgs))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2WebResourceResponseView))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2WindowFeatures))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2ContainsFullScreenElementChangedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2ContentLoadingEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2DocumentTitleChangedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2HistoryChangedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2NewWindowRequestedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2PermissionRequestedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2ProcessFailedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2ScriptDialogOpeningEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2SourceChangedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2WebMessageReceivedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2WebResourceRequestedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2WindowCloseRequestedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2AddScriptToExecuteOnDocumentCreatedCompletedHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2ExecuteScriptCompletedHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2CapturePreviewCompletedHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2CallDevToolsProtocolMethodCompletedHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2BrowserExtensionRemoveCompletedHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2BrowserExtensionEnableCompletedHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2CursorChangedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2NonClientRegionChangedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2CustomItemSelectedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2GetCookiesCompletedHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2DevToolsProtocolEventReceivedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2BytesReceivedChangedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2EstimatedEndTimeChangedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2StateChangedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2FindActiveMatchIndexChangedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2FindMatchCountChangedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2FindStartCompletedHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2FrameDestroyedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2FrameNameChangedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2FrameContentLoadingEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2FrameDOMContentLoadedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2FrameNavigationCompletedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2FrameNavigationStartingEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2FrameWebMessageReceivedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2FramePermissionRequestedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2FrameScreenCaptureStartingEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2FrameFrameCreatedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2NotificationCloseRequestedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2PrivateRemoteObjectProxyPassivatedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2ClearBrowsingDataCompletedHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2SetPermissionStateCompletedHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2GetNonDefaultPermissionSettingsCompletedHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2ProfileAddBrowserExtensionCompletedHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2ProfileGetBrowserExtensionsCompletedHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2ProfileDeletedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2WebResourceResponseViewGetContentCompletedHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2BasicAuthenticationRequestedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2ContextMenuRequestedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2StatusBarTextChangedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2ServerCertificateErrorDetectedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2ClearServerCertificateErrorActionsCompletedHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2FaviconChangedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2GetFaviconCompletedHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2PrintCompletedHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2PrintToPdfStreamCompletedHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2LaunchingExternalUriSchemeEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2DOMContentLoadedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2WebResourceResponseReceivedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2ExecuteScriptWithResultCompletedHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2SaveAsUIShowingEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2ShowSaveAsUICompletedHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2ScreenCaptureStartingEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2SaveFileSecurityCheckStartingEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2NotificationReceivedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2TrySuspendCompletedHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2DownloadStartingEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2FrameCreatedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2ClientCertificateRequestedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2PrintToPdfCompletedHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2IsDocumentPlayingAudioChangedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2IsMutedChangedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2IsDefaultDownloadDialogOpenChangedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2PhysicalKeyStatus))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.IDispatch", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Variant", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.JSHandlerWrapper", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.DelegateMap", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.EventConnector", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2InitializationCompletedEventArgs))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "typeof(Microsoft.Web.WebView2.Core.CoreWebView2WebObjectCollectionView)", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.COMStreamWrapper", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.BuiltInHostObject", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.COREWEBVIEW2_BROWSER_PROCESS_EXIT_KIND", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.COREWEBVIEW2_BROWSING_DATA_KINDS", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.COREWEBVIEW2_CAPTURE_PREVIEW_IMAGE_FORMAT", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.COREWEBVIEW2_CLIENT_CERTIFICATE_KIND", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.COREWEBVIEW2_CONTEXT_MENU_TARGET_KIND", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.COREWEBVIEW2_COOKIE_SAME_SITE_KIND", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.COREWEBVIEW2_DEFAULT_DOWNLOAD_DIALOG_CORNER_ALIGNMENT", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.COREWEBVIEW2_DOWNLOAD_INTERRUPT_REASON", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.COREWEBVIEW2_DOWNLOAD_STATE", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.COREWEBVIEW2_FAVICON_IMAGE_FORMAT", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.COREWEBVIEW2_FILE_SYSTEM_HANDLE_KIND", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.COREWEBVIEW2_FRAME_KIND", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.COREWEBVIEW2_HOST_RESOURCE_ACCESS_KIND", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.COREWEBVIEW2_KEY_EVENT_KIND", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.COREWEBVIEW2_MEMORY_USAGE_TARGET_LEVEL", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.COREWEBVIEW2_MOUSE_EVENT_KIND", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.COREWEBVIEW2_MOUSE_EVENT_VIRTUAL_KEYS", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.COREWEBVIEW2_NAVIGATION_KIND", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.COREWEBVIEW2_NON_CLIENT_REGION_KIND", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.COREWEBVIEW2_PDF_TOOLBAR_ITEMS", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.COREWEBVIEW2_PERMISSION_KIND", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.COREWEBVIEW2_PERMISSION_STATE", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.COREWEBVIEW2_PHYSICAL_KEY_STATUS", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.COREWEBVIEW2_POINTER_EVENT_KIND", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.COREWEBVIEW2_PREFERRED_COLOR_SCHEME", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.COREWEBVIEW2_PRINT_COLLATION", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.COREWEBVIEW2_PRINT_COLOR_MODE", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.COREWEBVIEW2_PRINT_DIALOG_KIND", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.COREWEBVIEW2_PRINT_DUPLEX", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.COREWEBVIEW2_PRINT_MEDIA_SIZE", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.COREWEBVIEW2_PRINT_ORIENTATION", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.COREWEBVIEW2_PRINT_STATUS", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.COREWEBVIEW2_PROCESS_FAILED_KIND", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.COREWEBVIEW2_PROCESS_FAILED_REASON", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.COREWEBVIEW2_PROCESS_KIND", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.COREWEBVIEW2_SAVE_AS_KIND", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.COREWEBVIEW2_SAVE_AS_UI_RESULT", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.COREWEBVIEW2_SCRIPT_DIALOG_KIND", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.COREWEBVIEW2_SERVER_CERTIFICATE_ERROR_ACTION", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.COREWEBVIEW2_SHARED_BUFFER_ACCESS", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.COREWEBVIEW2_TEXT_DIRECTION_KIND", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.COREWEBVIEW2_TRACKING_PREVENTION_LEVEL", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.COREWEBVIEW2_WEB_RESOURCE_CONTEXT", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.COREWEBVIEW2_WEB_RESOURCE_REQUEST_SOURCE_KINDS", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2AcceleratorKeyPressedEventArgs", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2AcceleratorKeyPressedEventArgs2", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2AddScriptToExecuteOnDocumentCreatedCompletedHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2BasicAuthenticationRequestedEventArgs", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2BasicAuthenticationRequestedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2BasicAuthenticationResponse", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2BrowserExtension", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2BrowserExtensionEnableCompletedHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2BrowserExtensionList", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2BrowserExtensionRemoveCompletedHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2BrowserProcessExitedEventArgs", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2BytesReceivedChangedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2CallDevToolsProtocolMethodCompletedHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2CapturePreviewCompletedHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2Certificate", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2ClearBrowsingDataCompletedHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2ClearServerCertificateErrorActionsCompletedHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2ClientCertificate", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2ClientCertificateCollection", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2ClientCertificateRequestedEventArgs", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2ClientCertificateRequestedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2CompositionController", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2CompositionController2", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2CompositionController3", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2CompositionController4", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2ContainsFullScreenElementChangedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2ContentLoadingEventArgs", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2ContentLoadingEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2ContextMenuItemCollection", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2ContextMenuRequestedEventArgs", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2ContextMenuRequestedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2ContextMenuTarget", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2ControllerOptions2", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2ControllerOptions3", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2ControllerOptions4", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2Cookie", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2CookieList", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2CookieManager", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2CursorChangedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2CustomItemSelectedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2DOMContentLoadedEventArgs", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2DOMContentLoadedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2Deferral", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2DevToolsProtocolEventReceivedEventArgs", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2DevToolsProtocolEventReceivedEventArgs2", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2DevToolsProtocolEventReceivedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2DevToolsProtocolEventReceiver", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2DocumentTitleChangedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2DownloadOperation", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2DownloadStartingEventArgs", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2DownloadStartingEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2EstimatedEndTimeChangedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2ExecuteScriptCompletedHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2ExecuteScriptResult", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2ExecuteScriptWithResultCompletedHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2FaviconChangedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2File", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2Find", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2FindActiveMatchIndexChangedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2FindMatchCountChangedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2FindStartCompletedHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2Frame", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2Frame2", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2Frame3", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2Frame4", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2Frame5", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2Frame6", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2Frame7", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2FrameChildFrameCreatedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2FrameContentLoadingEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2FrameCreatedEventArgs", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2FrameCreatedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2FrameDOMContentLoadedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2FrameDestroyedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2FrameInfo", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2FrameInfo2", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2FrameInfoCollection", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2FrameInfoCollectionIterator", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2FrameNameChangedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2FrameNavigationCompletedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2FrameNavigationStartingEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2FramePermissionRequestedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2FrameScreenCaptureStartingEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2FrameWebMessageReceivedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2GetCookiesCompletedHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2GetFaviconCompletedHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2GetNonDefaultPermissionSettingsCompletedHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2HistoryChangedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2HttpHeadersCollectionIterator", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2HttpRequestHeaders", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2HttpResponseHeaders", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2IsDefaultDownloadDialogOpenChangedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2IsDocumentPlayingAudioChangedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2IsMutedChangedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2LaunchingExternalUriSchemeEventArgs", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2LaunchingExternalUriSchemeEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2MoveFocusRequestedEventArgs", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2NavigationStartingEventArgs", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2NavigationStartingEventArgs2", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2NavigationStartingEventArgs3", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2NewWindowRequestedEventArgs", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2NewWindowRequestedEventArgs2", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2NewWindowRequestedEventArgs3", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2NewWindowRequestedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2NonClientRegionChangedEventArgs", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2NonClientRegionChangedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2Notification", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2NotificationCloseRequestedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2NotificationReceivedEventArgs", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2NotificationReceivedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2ObjectCollectionView", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2PermissionRequestedEventArgs", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2PermissionRequestedEventArgs2", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2PermissionRequestedEventArgs3", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2PermissionRequestedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2PermissionSetting", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2PermissionSettingCollectionView", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2PrintCompletedHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2PrintSettings2", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2PrintToPdfCompletedHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2PrintToPdfStreamCompletedHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2PrivateHostObjectAsyncMethodContinuation", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2PrivateKeyPressedEventArgs", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2PrivateLargeUnmanagedResource", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2PrivateRemoteObjectProxy", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2PrivateRemoteObjectProxyPassivatedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2ProcessExtendedInfo", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2ProcessExtendedInfoCollection", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2ProcessFailedEventArgs", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2ProcessFailedEventArgs2", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2ProcessFailedEventArgs3", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2ProcessFailedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2Profile", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2Profile2", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2Profile3", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2Profile4", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2Profile5", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2Profile6", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2Profile7", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2Profile8", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2ProfileAddBrowserExtensionCompletedHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2ProfileDeletedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2ProfileGetBrowserExtensionsCompletedHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2RegionRectCollectionView", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2SaveAsUIShowingEventArgs", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2SaveAsUIShowingEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2SaveFileSecurityCheckStartingEventArgs", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2SaveFileSecurityCheckStartingEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2ScreenCaptureStartingEventArgs", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2ScreenCaptureStartingEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2ScriptDialogOpeningEventArgs", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2ScriptDialogOpeningEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2ScriptException", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2ServerCertificateErrorDetectedEventArgs", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2ServerCertificateErrorDetectedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2SetPermissionStateCompletedHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2Settings2", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2Settings3", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2Settings4", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2Settings5", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2Settings6", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2Settings7", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2Settings8", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2Settings9", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2ShowSaveAsUICompletedHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2SourceChangedEventArgs", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2SourceChangedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2StateChangedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2StatusBarTextChangedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2StringCollection", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2TrySuspendCompletedHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2WebMessageReceivedEventArgs", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2WebMessageReceivedEventArgs2", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2WebMessageReceivedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2WebResourceRequestedEventArgs", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2WebResourceRequestedEventArgs2", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2WebResourceRequestedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2WebResourceResponseReceivedEventArgs", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2WebResourceResponseReceivedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2WebResourceResponseView", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2WebResourceResponseViewGetContentCompletedHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2WindowCloseRequestedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2WindowFeatures", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2_10", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2_11", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2_12", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2_13", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2_14", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2_15", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2_16", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2_17", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2_18", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2_19", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2_2", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2_20", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2_21", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2_22", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2_23", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2_24", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2_25", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2_26", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2_27", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2_28", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2_3", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2_4", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2_5", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2_6", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2_7", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2_8", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2_9", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.tagPOINT", "Microsoft.Web.WebView2.Core")]

    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.COMDotNetTypeConverter", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2AcceleratorKeyPressedEventArgs))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2BrowserProcessExitedEventArgs))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2CompositionController))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2ContextMenuItem))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2ControllerOptions))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2CustomSchemeRegistration))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2FileSystemHandle))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2FindOptions))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2HttpHeadersCollectionIterator))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2HttpRequestHeaders))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2MoveFocusRequestedEventArgs))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2NavigationStartingEventArgs))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2PointerInfo))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2PrintSettings))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2PrivateHostObjectHelper", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2PrivateKeyPressedEventArgs", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2ProcessExtendedInfo))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2ProcessInfo))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2Settings))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2SharedBuffer))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2WebResourceRequest))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2WebResourceResponse))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2NavigationStartingEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2AcceleratorKeyPressedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2FocusChangedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2MoveFocusRequestedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2ZoomFactorChangedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2RasterizationScaleChangedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2NewBrowserVersionAvailableEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2CreateCoreWebView2ControllerCompletedHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2GetProcessExtendedInfosCompletedHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2CreateCoreWebView2CompositionControllerCompletedHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2BrowserProcessExitedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2ProcessInfosChangedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.CoreWebView2PrivateKeyPressedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.HostObjectHelper))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.ManagedIStream", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.WebView2RuntimeNotFoundException))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2AcceleratorKeyPressedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2BrowserProcessExitedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2ContextMenuItem", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2Controller2", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2Controller3", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2Controller4", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2ControllerOptions", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2CreateCoreWebView2CompositionControllerCompletedHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2CreateCoreWebView2ControllerCompletedHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2CustomSchemeRegistration", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2Environment10", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2Environment11", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2Environment12", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2Environment13", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2Environment14", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2Environment15", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2Environment2", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2Environment3", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2Environment5", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2Environment6", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2Environment7", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2Environment8", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2Environment9", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2FileSystemHandle", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2FindOptions", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2FocusChangedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2GetProcessExtendedInfosCompletedHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2MoveFocusRequestedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2NavigationCompletedEventArgs2", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2NavigationCompletedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2NavigationStartingEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2NewBrowserVersionAvailableEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2PointerInfo", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2PrintSettings", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2PrivateHostObjectHelper", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2PrivateHostObjectHelper2", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2PrivateHostObjectHelper3", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2PrivateKeyPressedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2ProcessInfo", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2ProcessInfosChangedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2RasterizationScaleChangedEventHandler", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2Settings", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2SharedBuffer", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2WebResourceRequest", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2WebResourceResponse", "Microsoft.Web.WebView2.Core")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2ZoomFactorChangedEventHandler", "Microsoft.Web.WebView2.Core")]
    internal static Type RegisterWebView2DependencyType(this Type obj)
    {
        Console.WriteLine("register the dependency type of webview2 ");
        
        return obj;
    }

#if DEBUG
    public static void Main(string[] args = default!)
    {
        var box = new List<string>();

        var florDir = "/path/lib";

        using var assembly1 = Mono.Cecil.AssemblyDefinition.ReadAssembly(Path.Combine(florDir, "Microsoft.Web.WebView2.Core_0.dll"));
        using var assembly2 = Mono.Cecil.AssemblyDefinition.ReadAssembly(Path.Combine(florDir, "Microsoft.Web.WebView2.Core_1.dll"));

        CompareAssemblyInfo(assembly1, assembly2, m => box.Add(m));

        CompareTypesAndMethods(assembly1.MainModule, assembly2.MainModule, m => box.Add(m));

        var type1 = string.Join(Environment.NewLine, box.Where(m => m.Contains("缺失类型")).Select(m => m[(m.IndexOf("缺失类型") + 5)..]).Select(m =>
        {
            if (m.StartsWith("Microsoft.Web.WebView2.Core.Raw") || m.EndsWith("Handler"))
            {
                // [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2CreateCoreWebView2EnvironmentCompletedHandler", "Microsoft.Web.WebView2.Core")]
                return $"[DynamicDependency(DynamicallyAccessedMemberTypes.All, \"{m}\", \"Microsoft.Web.WebView2.Core\")]";
            }

            // [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2EnvironmentOptions))]
            return $"[DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof({m}))]";
        }).Distinct());
        var method1 = string.Join(Environment.NewLine, box.Where(m => m.Contains("缺失方法")).Select(m =>
        {
            m = m[..(m.IndexOf("缺失方法") - 4)][2..];

            if (m.StartsWith("Microsoft.Web.WebView2.Core.Raw") || m.EndsWith("Handler"))
            {
                // [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Microsoft.Web.WebView2.Core.Raw.ICoreWebView2CreateCoreWebView2EnvironmentCompletedHandler", "Microsoft.Web.WebView2.Core")]
                return $"[DynamicDependency(DynamicallyAccessedMemberTypes.All, \"{m}\", \"Microsoft.Web.WebView2.Core\")]";
            }

            // [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Microsoft.Web.WebView2.Core.CoreWebView2EnvironmentOptions))]
            return $"[DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof({m}))]";
        }).Distinct());

        var text = string.Join(Environment.NewLine, box);

        Console.WriteLine("对比完成！");
    }

    /// <summary>
    /// 对比程序集基本信息
    /// </summary>
    static void CompareAssemblyInfo(Mono.Cecil.AssemblyDefinition a1, Mono.Cecil.AssemblyDefinition a2, Action<string> ConsoleWriteLine)
    {
        ConsoleWriteLine("===== 程序集基本信息差异 =====");
        // 程序集名称+版本
        if (a1.Name.FullName != a2.Name.FullName)
        {
            ConsoleWriteLine($"程序集全名不同：\nA1: {a1.Name.FullName}\nA2: {a2.Name.FullName}");
        }
        // 目标框架（Mono.Cecil读取自定义属性中的TargetFramework）
        var tf1 = GetTargetFramework(a1);
        var tf2 = GetTargetFramework(a2);
        if (tf1 != tf2)
        {
            ConsoleWriteLine($"目标框架不同：\nA1: {tf1}\nA2: {tf2}");
        }
        // 强签名公钥（如果有）
        if (!a1.Name.PublicKeyToken.SequenceEqual(a2.Name.PublicKeyToken))
        {
            ConsoleWriteLine("程序集强签名公钥令牌不同！");
        }
    }

    /// <summary>
    /// 对比模块中的类型和方法
    /// </summary>
    static void CompareTypesAndMethods(Mono.Cecil.ModuleDefinition m1, Mono.Cecil.ModuleDefinition m2, Action<string> ConsoleWriteLine)
    {
        ConsoleWriteLine("\n===== 类型/方法结构差异 =====");
        // 获取两个程序集的所有类型（排除编译器生成的类型）
        var types1 = m1.Types.Where(t => true).ToDictionary(t => t.FullName);
        var types2 = m2.Types.Where(t => true).ToDictionary(t => t.FullName);

        // 1. A1有但A2没有的类型
        foreach (var typeName in types1.Keys.Except(types2.Keys))
        {
            ConsoleWriteLine($"A2缺失类型：{typeName}");
        }

        // 2. A2有但A1没有的类型
        foreach (var typeName in types2.Keys.Except(types1.Keys))
        {
            ConsoleWriteLine($"A1缺失类型：{typeName}");
        }

        // 3. 双方都有的类型，对比其中的方法
        foreach (var typeName in types1.Keys.Intersect(types2.Keys))
        {
            var t1 = types1[typeName];
            var t2 = types2[typeName];
            var methods1 = t1.Methods.Where(m => true).ToDictionary(m => m.FullName);
            var methods2 = t2.Methods.Where(m => true).ToDictionary(m => m.FullName);

            // 方法缺失对比
            foreach (var methodName in methods1.Keys.Except(methods2.Keys))
            {
                ConsoleWriteLine($"类型{typeName}中，A2缺失方法：{methodName}");
            }
            foreach (var methodName in methods2.Keys.Except(methods1.Keys))
            {
                ConsoleWriteLine($"类型{typeName}中，A1缺失方法：{methodName}");
            }
        }
    }

    /// <summary>
    /// 从程序集自定义属性中获取目标框架（如.NETCoreApp,Version=v6.0）
    /// </summary>
    static string GetTargetFramework(Mono.Cecil.AssemblyDefinition assembly)
    {
        var attr = assembly.CustomAttributes
            .FirstOrDefault(a => a.AttributeType.FullName == "System.Runtime.Versioning.TargetFrameworkAttribute");
        if (attr == null) return "未知";
        return attr.ConstructorArguments[0].Value?.ToString() ?? "未知";
    }
#endif
}
