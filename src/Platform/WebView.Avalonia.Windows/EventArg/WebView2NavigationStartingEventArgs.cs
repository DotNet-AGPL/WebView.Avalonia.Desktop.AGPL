using Microsoft.Web.WebView2.Core;
using WebView.Avalonia.Core.Models;

namespace WebView.Avalonia.Windows.EventArg;

internal class WebView2NavigationStartingEventArgs : NavigationStartingEventArgs
{
    private CoreWebView2NavigationStartingEventArgs eventArgs;

    private HttpRequestHeaders requestHeaders;
    private NavigationKind navigationKind;

    internal WebView2NavigationStartingEventArgs(CoreWebView2NavigationStartingEventArgs eventArgs)
    {
        this.eventArgs = eventArgs;

        this.requestHeaders = Convert(eventArgs.RequestHeaders);
        this.navigationKind = Convert(eventArgs.NavigationKind);
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

    public override HttpRequestHeaders RequestHeaders => requestHeaders;
    
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

    public override NavigationKind NavigationKind => navigationKind;

    private static HttpRequestHeaders Convert(CoreWebView2HttpRequestHeaders coreWebView2HttpRequestHeaders) 
    {
        var requestHeaders = new HttpRequestHeaders();

        return requestHeaders;
    }

    private static NavigationKind Convert(CoreWebView2NavigationKind coreWebView2NavigationKind)
    {
        var navigationKind = new NavigationKind();

        return navigationKind;
    }
}
