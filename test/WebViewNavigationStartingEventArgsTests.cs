using WebView.Avalonia.Core.Models;

namespace WebView.Avalonia.Core.Tests.Models;

/// <summary>
/// Tests for WebViewNavigationStartingEventArgs abstract class
/// </summary>
public class WebViewNavigationStartingEventArgsTests
{
    [Fact]
    public void WebViewNavigationStartingEventArgs_ConcreteImplementation_CanBeCreated()
    {
        // Arrange & Act
        var args = new TestWebViewNavigationStartingEventArgs();
        
        // Assert - can create concrete implementation
        Assert.NotNull(args);
    }

    /// <summary>
    /// Test implementation of WebViewNavigationStartingEventArgs with stub implementations
    /// </summary>
    private class TestWebViewNavigationStartingEventArgs : WebViewNavigationStartingEventArgs
    {
        public override bool Cancel { get; set; }
        
        public override bool IsRedirected => false;
        
        public override bool IsUserInitiated => false;
        
        public override ulong NavigationId => 0;
        
        public override WebViewHttpRequestHeaders RequestHeaders => new TestWebViewHttpRequestHeaders();
        
        public override string Uri => string.Empty;
        
        public override string AdditionalAllowedFrameAncestors { get; set; } = string.Empty;
        
        public override WebViewNavigationKind NavigationKind => WebViewNavigationKind.NewDocument;
    }

    private class TestWebViewHttpRequestHeaders : WebViewHttpRequestHeaders
    {
        public override string GetHeader(string name) => string.Empty;
        
        public override IEnumerator<KeyValuePair<string, string>> GetHeaders(string name) 
            => Enumerable.Empty<KeyValuePair<string, string>>().GetEnumerator();
        
        public override bool Contains(string name) => false;
        
        public override void SetHeader(string name, string value) { }
        
        public override void RemoveHeader(string name) { }
    }
}
