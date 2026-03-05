using WebKit;
using WebView.Avalonia.Core.Models;

namespace WebView.Avalonia.Linux.Models;

/// <summary>
/// Linux WebView navigation starting event args
/// </summary>
public class WebKitWebViewNavigationStartingEventArgs : WebViewNavigationStartingEventArgs
{
    private WebKit.WebView? webViewInstance;
    private LoadChangedArgs eventArgs;

    private WebViewHttpRequestHeaders requestHeaders;
    private WebViewNavigationKind navigationKind;

    public WebKitWebViewNavigationStartingEventArgs(WebKit.WebView? webViewInstance, LoadChangedArgs eventArgs)
    {
        this.webViewInstance = webViewInstance;
        this.eventArgs = eventArgs;
        this.requestHeaders = new WebKitWebViewHttpRequestHeaders();
        this.navigationKind = WebViewNavigationKind.None;
    }

    public override bool Cancel { get; set; }

    public override bool IsRedirected { get; }

    public override bool IsUserInitiated { get; }

    public override ulong NavigationId { get; }

    public override WebViewHttpRequestHeaders RequestHeaders => requestHeaders;

    public override string Uri => webViewInstance?.Uri ?? string.Empty;

    public override string AdditionalAllowedFrameAncestors { get; set; } = string.Empty;

    public override WebViewNavigationKind NavigationKind => navigationKind;
}

/// <summary>
/// Linux WebView HTTP request headers implementation
/// </summary>
public class WebKitWebViewHttpRequestHeaders : WebViewHttpRequestHeaders
{
    internal WebKitWebViewHttpRequestHeaders()
    {
    }

    //
    // Gets the header value matching the name.
    public override string GetHeader(string name) => string.Empty;

    //
    //  Gets the header value matching the name using a Microsoft.Web.WebView2.Core.CoreWebView2HttpHeadersCollectionIterator.
    //  The header value matching the name.
    public override IEnumerator<KeyValuePair<string, string>> GetHeaders(string name) => new List<KeyValuePair<string, string>>(0).GetEnumerator();

    //
    //  Checks whether the headers contain an entry that matches the header name.
    public override bool Contains(string name) => false;

    //
    //  Adds or updates header that matches the name.
    public override void SetHeader(string name, string value) { }

    //
    //  Removes header that matches the name.
    public override void RemoveHeader(string name) { }
}
