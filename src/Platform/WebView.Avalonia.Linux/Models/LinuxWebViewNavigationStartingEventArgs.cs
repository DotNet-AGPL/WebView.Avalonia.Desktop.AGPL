using WebView.Avalonia.Core.Models;

namespace WebView.Avalonia.Linux.Models;

/// <summary>
/// Linux WebView navigation starting event args
/// </summary>
public class LinuxWebViewNavigationStartingEventArgs : WebViewNavigationStartingEventArgs
{
    private readonly string _uri;
    private readonly bool _isRedirected;
    private readonly bool _isUserInitiated;
    private readonly ulong _navigationId;
    private readonly WebViewHttpRequestHeaders _requestHeaders;
    private readonly WebViewNavigationKind _navigationKind;
    private bool _cancel;

    public LinuxWebViewNavigationStartingEventArgs(
        string uri,
        bool isRedirected = false,
        bool isUserInitiated = false,
        ulong navigationId = 0,
        WebViewHttpRequestHeaders? requestHeaders = null,
        WebViewNavigationKind navigationKind = WebViewNavigationKind.NewDocument)
    {
        _uri = uri;
        _isRedirected = isRedirected;
        _isUserInitiated = isUserInitiated;
        _navigationId = navigationId;
        _requestHeaders = requestHeaders ?? new LinuxWebViewHttpRequestHeaders();
        _navigationKind = navigationKind;
    }

    public override bool Cancel
    {
        get => _cancel;
        set => _cancel = value;
    }

    public override bool IsRedirected => _isRedirected;

    public override bool IsUserInitiated => _isUserInitiated;

    public override ulong NavigationId => _navigationId;

    public override WebViewHttpRequestHeaders RequestHeaders => _requestHeaders;

    public override string Uri => _uri;

    public override string AdditionalAllowedFrameAncestors { get; set; } = string.Empty;

    public override WebViewNavigationKind NavigationKind => _navigationKind;
}

/// <summary>
/// Linux WebView HTTP request headers implementation
/// </summary>
public class LinuxWebViewHttpRequestHeaders : WebViewHttpRequestHeaders
{
    private readonly Dictionary<string, string> _headers = new(StringComparer.OrdinalIgnoreCase);

    public override string GetHeader(string name)
    {
        return _headers.TryGetValue(name, out var value) ? value : string.Empty;
    }

    public override IEnumerator<KeyValuePair<string, string>> GetHeaders(string name)
    {
        // For headers with multiple values, iterate through them
        if (_headers.TryGetValue(name, out var value))
        {
            yield return new KeyValuePair<string, string>(name, value);
        }
    }

    public override bool Contains(string name)
    {
        return _headers.ContainsKey(name);
    }

    public override void SetHeader(string name, string value)
    {
        _headers[name] = value;
    }

    public override void RemoveHeader(string name)
    {
        _headers.Remove(name);
    }

    internal void AddHeader(string name, string value)
    {
        _headers[name] = value;
    }
}
