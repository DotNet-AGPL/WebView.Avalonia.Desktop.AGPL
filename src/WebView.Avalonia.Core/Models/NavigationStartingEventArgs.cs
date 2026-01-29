namespace WebView.Avalonia.Core.Models;

public abstract class NavigationStartingEventArgs
{
    public abstract bool Cancel { get; set; }

    public abstract bool IsRedirected { get; }

    public abstract bool IsUserInitiated { get; }

    public abstract ulong NavigationId { get; }

    public abstract HttpRequestHeaders? RequestHeaders { get; }

    public abstract string Uri { get; }

    public abstract string AdditionalAllowedFrameAncestors { get; set; }

    public abstract NavigationKind NavigationKind { get; }
}
