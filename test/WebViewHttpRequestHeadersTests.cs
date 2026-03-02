using WebView.Avalonia.Core.Models;

namespace WebView.Avalonia.Core.Tests.Models;

/// <summary>
/// Tests for WebViewHttpRequestHeaders abstract class
/// </summary>
public class WebViewHttpRequestHeadersTests
{
    [Fact]
    public void WebViewHttpRequestHeaders_ConcreteImplementation_CanBeCreated()
    {
        // Arrange & Act
        var headers = new TestWebViewHttpRequestHeaders();
        
        // Assert - can create concrete implementation
        Assert.NotNull(headers);
    }

    /// <summary>
    /// Test implementation of WebViewHttpRequestHeaders with stub implementations
    /// </summary>
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
