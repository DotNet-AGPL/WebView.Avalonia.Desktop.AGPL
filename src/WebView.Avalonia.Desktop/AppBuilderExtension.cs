using Avalonia;

namespace WebView.Avalonia.Desktop;

public static class AppBuilderExtension
{
    public static AppBuilder UseWinJitWebView2(this AppBuilder appBuilder) 
    {
        WebView.Avalonia.Windows.AppBuilderExtension.UseWinJitWebView2(appBuilder);
        
        return appBuilder;
    }
}
