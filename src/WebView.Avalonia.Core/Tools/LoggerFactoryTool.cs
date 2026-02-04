using Microsoft.Extensions.Logging;
using Serilog;

namespace WebView.Avalonia.Core.Tools;

public static class LoggerFactoryTool
{
    private static readonly ILoggerFactory loggerFactory;

    static LoggerFactoryTool() {
        var outputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] [{SourceContext:l}] {Message:lj}{NewLine}{Exception}";

        // 1. 配置 Serilog（定义日志输出目标、级别、格式等）
        var serilogLogger = new LoggerConfiguration()
            .MinimumLevel.Information() // 全局最小日志级别
            .WriteTo.File("logs/app.log", rollingInterval: RollingInterval.Day, outputTemplate: outputTemplate) // 输出到文件（按天滚动）
            .CreateLogger();

        // 2. 创建 MEL 的 LoggerFactory，并添加 Serilog 作为日志提供器
        loggerFactory = LoggerFactory.Create(builder =>
        {
            // 清除 MEL 默认的日志提供器（可选，避免重复输出）
            builder.ClearProviders();
            // 添加 Serilog 提供器（核心：将 Serilog 接入 MEL）
            builder.AddSerilog(serilogLogger);
        });
    }

    public static Microsoft.Extensions.Logging.ILogger GetLogger<T>() where T : class => loggerFactory.CreateLogger<T>();
}
