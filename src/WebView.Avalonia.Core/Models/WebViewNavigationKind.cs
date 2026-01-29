using System.Diagnostics.CodeAnalysis;

namespace WebView.Avalonia.Core.Models;

public enum WebViewNavigationKind
{
    // 摘要:
    //     A navigation caused by `WebView.Reload()`, `location.reload()`, the end
    //     user using F5 or other UX, or other reload mechanisms to reload the current document
    //     without modifying the navigation history.
    Reload,
    //
    // 摘要:
    //     A navigation back or forward to a different entry in the session navigation history,
    //     like via `WebView.Back()`, `location.back()`, the end user pressing Alt+Left
    //     or other UX, or other mechanisms to navigate back or forward in the current session
    //     navigation history.
    BackOrForward,
    //
    // 摘要:
    //     A navigation to another document, which can be caused by `WebView.Navigate()`,
    //     `window.location.href = ...`, or other WebView2 or DOM APIs that navigate to
    //     a new URI.
    NewDocument
}
