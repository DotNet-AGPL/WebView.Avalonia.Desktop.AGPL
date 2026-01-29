namespace WebView.Avalonia.Core.Models;

public abstract class WebViewHttpRequestHeaders
{
    //
    // 摘要:
    //     Gets the header value matching the name.
    public abstract string GetHeader(string name);

    //
    // 摘要:
    //     Gets the header value matching the name using a Microsoft.Web.WebView2.Core.CoreWebView2HttpHeadersCollectionIterator.
    //     The header value matching the name.
    public abstract IEnumerator<KeyValuePair<string, string>> GetHeaders(string name);

    //
    // 摘要:
    //     Checks whether the headers contain an entry that matches the header name.
    public abstract bool Contains(string name);

    //
    // 摘要:
    //     Adds or updates header that matches the name.
    public abstract void SetHeader(string name, string value);

    //
    // 摘要:
    //     Removes header that matches the name.
    public abstract void RemoveHeader(string name);
}
