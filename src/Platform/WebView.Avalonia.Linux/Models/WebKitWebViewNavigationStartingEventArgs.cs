using System.Collections.Generic;
using WebKit;
using WebView.Avalonia.Core.Models;

namespace WebView.Avalonia.Linux.Models;

/// <summary>
/// Linux WebView navigation starting event args
/// </summary>
public class WebKitWebViewNavigationStartingEventArgs : WebViewNavigationStartingEventArgs
{
    /// <summary>
    /// WebKit WebView instance
    /// </summary>
    private WebKit.WebView? webViewInstance;
    
    /// <summary>
    /// Load changed event arguments from WebKit
    /// </summary>
    private LoadChangedArgs eventArgs;

    /// <summary>
    /// HTTP request headers
    /// </summary>
    private WebViewHttpRequestHeaders requestHeaders;
    
    /// <summary>
    /// Navigation kind (e.g., link clicked, form submitted, etc.)
    /// </summary>
    private WebViewNavigationKind navigationKind;

    /// <summary>
    /// Creates a new instance of WebKitWebViewNavigationStartingEventArgs
    /// </summary>
    /// <param name="webViewInstance">The WebKit WebView instance</param>
    /// <param name="eventArgs">The load changed event arguments from WebKit</param>
    public WebKitWebViewNavigationStartingEventArgs(WebKit.WebView? webViewInstance, LoadChangedArgs eventArgs)
    {
        this.webViewInstance = webViewInstance;
        this.eventArgs = eventArgs;
        this.requestHeaders = new WebKitWebViewHttpRequestHeaders();
        this.navigationKind = WebViewNavigationKind.None;
    }

    /// <summary>
    /// Gets or sets whether the navigation should be cancelled
    /// </summary>
    public override bool Cancel { get; set; }

    /// <summary>
    /// Gets whether the navigation was redirected
    /// </summary>
    public override bool IsRedirected { get; }

    /// <summary>
    /// Gets whether the navigation was initiated by the user
    /// </summary>
    public override bool IsUserInitiated { get; }

    /// <summary>
    /// Gets the navigation identifier
    /// </summary>
    public override ulong NavigationId { get; }

    /// <summary>
    /// Gets the HTTP request headers
    /// </summary>
    public override WebViewHttpRequestHeaders RequestHeaders => requestHeaders;

    /// <summary>
    /// Gets the URI being navigated to
    /// </summary>
    public override string Uri => webViewInstance?.Uri ?? string.Empty;

    /// <summary>
    /// Gets or sets additional allowed frame ancestors
    /// </summary>
    public override string AdditionalAllowedFrameAncestors { get; set; } = string.Empty;

    /// <summary>
    /// Gets the kind of navigation
    /// </summary>
    public override WebViewNavigationKind NavigationKind => navigationKind;
}

/// <summary>
/// Linux WebView HTTP request headers implementation
/// </summary>
public class WebKitWebViewHttpRequestHeaders : WebViewHttpRequestHeaders
{
    /// <summary>
    /// Creates a new instance of WebKitWebViewHttpRequestHeaders
    /// </summary>
    internal WebKitWebViewHttpRequestHeaders()
    {
    }

    /// <summary>
    /// Gets the header value matching the name.
    /// </summary>
    /// <param name="name">The header name</param>
    /// <returns>The header value, or empty string if not found</returns>
    public override string GetHeader(string name) => string.Empty;

    /// <summary>
    /// Gets the header values matching the name using a WebKit iterator.
    /// </summary>
    /// <param name="name">The header name</param>
    /// <returns>An enumerator of header key-value pairs</returns>
    public override IEnumerator<KeyValuePair<string, string>> GetHeaders(string name) => new List<KeyValuePair<string, string>>(0).GetEnumerator();

    /// <summary>
    /// Checks whether the headers contain an entry that matches the header name.
    /// </summary>
    /// <param name="name">The header name to check</param>
    /// <returns>True if the header exists, false otherwise</returns>
    public override bool Contains(string name) => false;

    /// <summary>
    /// Adds or updates header that matches the name.
    /// </summary>
    /// <param name="name">The header name</param>
    /// <param name="value">The header value</param>
    public override void SetHeader(string name, string value) { }

    /// <summary>
    /// Removes header that matches the name.
    /// </summary>
    /// <param name="name">The header name to remove</param>
    public override void RemoveHeader(string name) { }
}
