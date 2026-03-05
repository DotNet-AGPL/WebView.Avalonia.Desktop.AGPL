using WebView.Avalonia.Core.Models;

namespace WebView.Avalonia.Mac.Models;

/// <summary>
/// macOS WebView navigation completed event args
/// </summary>
public class MacWebViewNavigationCompletedEventArgs : WebViewNavigationCompletedEventArgs
{
    private readonly bool _isSuccess;
    private readonly ulong _navigationId;
    private readonly WebViewWebErrorStatus _webErrorStatus;
    private readonly int _httpStatusCode;
    private readonly string? _errorMessage;

    public MacWebViewNavigationCompletedEventArgs(
        bool isSuccess,
        ulong navigationId = 0,
        WebViewWebErrorStatus webErrorStatus = WebViewWebErrorStatus.Unknown,
        int httpStatusCode = 0,
        string? errorMessage = null)
    {
        _isSuccess = isSuccess;
        _navigationId = navigationId;
        _webErrorStatus = webErrorStatus;
        _httpStatusCode = httpStatusCode;
        _errorMessage = errorMessage;
    }

    public override bool IsSuccess => _isSuccess;

    public override ulong NavigationId => _navigationId;

    public override WebViewWebErrorStatus WebErrorStatus => _webErrorStatus;

    public override int HttpStatusCode => _httpStatusCode;

    /// <summary>
    /// Gets the error message if navigation failed
    /// </summary>
    public string? ErrorMessage => _errorMessage;
}
