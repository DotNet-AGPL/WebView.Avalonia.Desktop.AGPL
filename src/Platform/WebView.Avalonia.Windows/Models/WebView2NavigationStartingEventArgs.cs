using System.Diagnostics.CodeAnalysis;
using Microsoft.Web.WebView2.Core;
using WebView.Avalonia.Core.Models;

namespace WebView.Avalonia.Windows.Models;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
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

    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
    internal class WebView2HttpRequestHeaders : WebViewHttpRequestHeaders
    {
        private readonly CoreWebView2HttpRequestHeaders coreWebView2HttpRequestHeaders;

        internal WebView2HttpRequestHeaders(CoreWebView2HttpRequestHeaders coreWebView2HttpRequestHeaders)
        {
            this.coreWebView2HttpRequestHeaders = coreWebView2HttpRequestHeaders;
        }

        //
        // Gets the header value matching the name.
        public override string GetHeader(string name) => coreWebView2HttpRequestHeaders.GetHeader(name);

        //
        //  Gets the header value matching the name using a Microsoft.Web.WebView2.Core.CoreWebView2HttpHeadersCollectionIterator.
        //  The header value matching the name.
        public override IEnumerator<KeyValuePair<string, string>> GetHeaders(string name) => coreWebView2HttpRequestHeaders.GetHeaders(name);

        //
        //  Checks whether the headers contain an entry that matches the header name.
        public override bool Contains(string name) => coreWebView2HttpRequestHeaders.Contains(name);

        //
        //  Adds or updates header that matches the name.
        public override void SetHeader(string name, string value) => coreWebView2HttpRequestHeaders.SetHeader(name, value);

        //
        //  Removes header that matches the name.
        public override void RemoveHeader(string name) => coreWebView2HttpRequestHeaders.RemoveHeader(name);
    }
}
