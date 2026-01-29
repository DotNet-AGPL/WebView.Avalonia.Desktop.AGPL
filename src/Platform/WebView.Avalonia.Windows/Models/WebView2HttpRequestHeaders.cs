using Microsoft.Web.WebView2.Core;
using WebView.Avalonia.Core.Models;

namespace WebView.Avalonia.Windows.Models
{
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
