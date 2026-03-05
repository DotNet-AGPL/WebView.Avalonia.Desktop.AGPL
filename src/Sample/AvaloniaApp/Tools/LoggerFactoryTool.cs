namespace AvaloniaApp.Tools
{
    using Microsoft.Extensions.Logging;
    using Serilog;

    public static class LoggerFactoryTool
    {
        private static readonly ILoggerFactory loggerFactory;

        static LoggerFactoryTool() {
            // 1. 配置 Serilog（定义日志输出目标、级别、格式等）
            var serilogLogger = new LoggerConfiguration()
                .MinimumLevel.Information() // 全局最小日志级别
                .WriteTo.File("logs/app.log", rollingInterval: RollingInterval.Day) // 输出到文件（按天滚动）
                .CreateLogger();

            // 2. 创建 MEL 的 LoggerFactory，并添加 Serilog 作为日志提供器
            loggerFactory = LoggerFactory.Create(builder =>
            {
                // 清除 MEL 默认的日志提供器（可选，避免重复输出）
                builder.ClearProviders();
                // 添加 Serilog 提供器（核心：将 Serilog 接入 MEL）
                builder.AddSerilog(serilogLogger);
            });

            /*
            // <TrimmerRootAssembly Include="System.Drawing" />
            var list = Directory.GetFiles("E:\\Z_E_Data\\Github\\Repos\\DotNet-AGPL\\WebView.Avalonia.Destop.AGPL\\.output\\AvaloniaApp\\bin\\net10.0\\publish\\scd_notrim_win-x64")
                .Select(m => Path.GetFileName(m))
                .Where(m=> m.EndsWith(".dll"))
                .Select(m=> m.Substring(0, m.LastIndexOf(".dll")))
                .Select(m=> $"<TrimmerRootAssembly Include=\"{m}\" />")
                .ToList();

            var str = string.Join(Environment.NewLine, list);

            var temp = 123;
            */
            
        }

        public static ILoggerFactory GetLoggerFactory() => loggerFactory;
    }
}
