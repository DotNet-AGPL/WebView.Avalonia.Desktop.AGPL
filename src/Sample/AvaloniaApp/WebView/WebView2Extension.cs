using Avalonia.Controls;
using Microsoft.Web.WebView2.Core;

namespace AvaloniaApp.WebView
{
    internal static class WebView2Extension
    {
        public static void ResetWebViewSize(this CoreWebView2Controller coreWebView2Controller, Control control)
        {
            if (coreWebView2Controller is null)
            {
                return;
            }

            var scale = TopLevel.GetTopLevel(control)?.RenderScaling ?? 1;

            coreWebView2Controller.Bounds = new System.Drawing.Rectangle(0, 0, Convert.ToInt32(control.Bounds.Width * scale), Convert.ToInt32(control.Bounds.Height * scale));
            coreWebView2Controller.NotifyParentWindowPositionChanged();

            return;
        }
    }
}
