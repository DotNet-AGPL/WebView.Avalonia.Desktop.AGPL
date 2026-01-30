using System.Diagnostics.CodeAnalysis;

namespace WebView.Avalonia.Core.Models;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
public class WebViewNavigationCompletedEventArgs : EventArgs
{
    public virtual bool IsSuccess { get; }

    //
    //     Gets the ID of the navigation.
    public virtual ulong NavigationId { get; }

    //
    //     Gets the error code if the navigation failed.
    public virtual WebViewWebErrorStatus WebErrorStatus { get; }

    //
    // 摘要:
    //     The HTTP status code of the navigation if it involved an HTTP request. For instance,
    //     this will usually be 200 if the request was successful, 404 if a page was not
    //     found, etc. See https://developer.mozilla.org/docs/Web/HTTP/Status for a list
    //     of common status codes.
    public virtual int HttpStatusCode { get; }
}
