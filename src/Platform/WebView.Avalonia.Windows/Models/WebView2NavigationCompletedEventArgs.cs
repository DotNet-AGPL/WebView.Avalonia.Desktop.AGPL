using System.Diagnostics.CodeAnalysis;
using Microsoft.Web.WebView2.Core;
using WebView.Avalonia.Core.Models;

namespace WebView.Avalonia.Windows.Models;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
internal class WebView2NavigationCompletedEventArgs : WebViewNavigationCompletedEventArgs
{
    private CoreWebView2NavigationCompletedEventArgs eventArgs;

    private readonly WebViewWebErrorStatus webErrorStatus;

    internal WebView2NavigationCompletedEventArgs(CoreWebView2NavigationCompletedEventArgs eventArgs)
    {
        this.eventArgs = eventArgs;

        // 获取源枚举名称
        this.webErrorStatus = Enum.TryParse(Enum.GetName(eventArgs.WebErrorStatus), ignoreCase: true, out WebViewWebErrorStatus result) ? result : default!;
    }

    public override bool IsSuccess => eventArgs.IsSuccess;

    //
    //     Gets the ID of the navigation.
    public override ulong NavigationId => eventArgs.NavigationId;

    //
    //     Gets the error code if the navigation failed.
    public override WebViewWebErrorStatus WebErrorStatus => webErrorStatus;

    //
    // 摘要:
    //     The HTTP status code of the navigation if it involved an HTTP request. For instance,
    //     this will usually be 200 if the request was successful, 404 if a page was not
    //     found, etc. See https://developer.mozilla.org/docs/Web/HTTP/Status for a list
    //     of common status codes.
    public override int HttpStatusCode => eventArgs.HttpStatusCode;
}
