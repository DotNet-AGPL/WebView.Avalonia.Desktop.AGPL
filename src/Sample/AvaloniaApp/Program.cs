using System;
using System.Diagnostics.CodeAnalysis;
using Avalonia;
using AvaloniaApp.Tools;
using Microsoft.Extensions.Logging;
using Microsoft.Web.WebView2.Core;


namespace AvaloniaApp
{
    internal class Program
    {
        private static ILogger<Program> logger = LoggerFactoryTool.GetLogger<Program>();

        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        [STAThread]
        public static void Main(string[] args) 
        {
            try
            {
                BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
            }
            catch (Exception ex) 
            {
                logger.LogInformation(ex.Message);
                logger.LogInformation(ex.StackTrace);
            }
        }

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .WithInterFont()
                .LogToTrace();
    }
}
