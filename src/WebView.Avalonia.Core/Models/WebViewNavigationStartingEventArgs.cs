using System.Diagnostics.CodeAnalysis;

namespace WebView.Avalonia.Core.Models;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
public abstract class WebViewNavigationStartingEventArgs
{
    public abstract bool Cancel { get; set; }

    public abstract bool IsRedirected { get; }

    public abstract bool IsUserInitiated { get; }

    public abstract ulong NavigationId { get; }

    public abstract WebViewHttpRequestHeaders RequestHeaders { get; }

    public abstract string Uri { get; }

    public abstract string AdditionalAllowedFrameAncestors { get; set; }

    public abstract WebViewNavigationKind NavigationKind { get; }
}
