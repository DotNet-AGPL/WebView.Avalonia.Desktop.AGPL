using WebView.Avalonia.Core.Models;

namespace WebView.Avalonia.Core.Tests.Models;

/// <summary>
/// Tests for WebViewNavigationCompletedEventArgs concrete class
/// </summary>
public class WebViewNavigationCompletedEventArgsTests
{
    [Fact]
    public void WebViewNavigationCompletedEventArgs_DefaultValues_AreCorrect()
    {
        // Arrange & Act
        var args = new TestWebViewNavigationCompletedEventArgs();
        
        // Assert - default values
        Assert.False(args.IsSuccess);
        Assert.Equal(0UL, args.NavigationId);
        Assert.Equal(WebViewWebErrorStatus.Unknown, args.WebErrorStatus);
        Assert.Equal(0, args.HttpStatusCode);
    }

    [Fact]
    public void WebViewNavigationCompletedEventArgs_CanBeConstructedWithValues()
    {
        // Arrange & Act
        var args = new TestWebViewNavigationCompletedEventArgs(
            isSuccess: true,
            navigationId: 12345UL,
            webErrorStatus: WebViewWebErrorStatus.CertificateExpired,
            httpStatusCode: 200
        );
        
        // Assert
        Assert.True(args.IsSuccess);
        Assert.Equal(12345UL, args.NavigationId);
        Assert.Equal(WebViewWebErrorStatus.CertificateExpired, args.WebErrorStatus);
        Assert.Equal(200, args.HttpStatusCode);
    }

    [Fact]
    public void WebViewNavigationCompletedEventArgs_FailedNavigation_HasCorrectValues()
    {
        // Arrange & Act
        var args = new TestWebViewNavigationCompletedEventArgs(
            isSuccess: false,
            navigationId: 999UL,
            webErrorStatus: WebViewWebErrorStatus.CannotConnect,
            httpStatusCode: 0
        );
        
        // Assert
        Assert.False(args.IsSuccess);
        Assert.Equal(999UL, args.NavigationId);
        Assert.Equal(WebViewWebErrorStatus.CannotConnect, args.WebErrorStatus);
        Assert.Equal(0, args.HttpStatusCode);
    }

    /// <summary>
    /// Test implementation of WebViewNavigationCompletedEventArgs for testing purposes
    /// </summary>
    private class TestWebViewNavigationCompletedEventArgs : WebViewNavigationCompletedEventArgs
    {
        public TestWebViewNavigationCompletedEventArgs() { }

        public TestWebViewNavigationCompletedEventArgs(
            bool isSuccess,
            ulong navigationId,
            WebViewWebErrorStatus webErrorStatus,
            int httpStatusCode)
        {
            _isSuccess = isSuccess;
            _navigationId = navigationId;
            _webErrorStatus = webErrorStatus;
            _httpStatusCode = httpStatusCode;
        }

        private bool _isSuccess;
        private ulong _navigationId;
        private WebViewWebErrorStatus _webErrorStatus;
        private int _httpStatusCode;

        public override bool IsSuccess => _isSuccess;
        public override ulong NavigationId => _navigationId;
        public override WebViewWebErrorStatus WebErrorStatus => _webErrorStatus;
        public override int HttpStatusCode => _httpStatusCode;
    }
}
