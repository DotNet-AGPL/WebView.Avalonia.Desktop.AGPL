using Microsoft.Web.WebView2.Core;
using WebView.Avalonia.Core.Models;

namespace WebView.Avalonia.Windows.Models;

internal class WebView2NavigationStartingEventArgs : WebViewNavigationStartingEventArgs
{
    private CoreWebView2NavigationStartingEventArgs eventArgs;

    private WebViewHttpRequestHeaders requestHeaders;
    private WebViewNavigationKind navigationKind;

    internal WebView2NavigationStartingEventArgs(CoreWebView2NavigationStartingEventArgs eventArgs)
    {
        this.eventArgs = eventArgs;

        this.requestHeaders = new WebView2HttpRequestHeaders(eventArgs.RequestHeaders);

        this.navigationKind = eventArgs.NavigationKind switch
        {
            CoreWebView2NavigationKind.Reload => WebViewNavigationKind.Reload,
            CoreWebView2NavigationKind.BackOrForward => WebViewNavigationKind.BackOrForward,
            CoreWebView2NavigationKind.NewDocument => WebViewNavigationKind.NewDocument,
            _ => default
        };
    }

    public override bool Cancel
    {
        get
        {
            return eventArgs.Cancel;
        }
        set
        {
            eventArgs.Cancel = value;
        }
    }

    public override bool IsRedirected => eventArgs.IsRedirected;

    public override bool IsUserInitiated => eventArgs.IsUserInitiated;

    public override ulong NavigationId => eventArgs.NavigationId;

    public override WebViewHttpRequestHeaders RequestHeaders => requestHeaders;
    
    public override string Uri => eventArgs.Uri;

    public override string AdditionalAllowedFrameAncestors
    {
        get
        {
            return eventArgs.AdditionalAllowedFrameAncestors;
        }
        set
        {
            eventArgs.AdditionalAllowedFrameAncestors = value;
        }
    }

    public override WebViewNavigationKind NavigationKind => navigationKind;
}
