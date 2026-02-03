using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;

namespace WebView.Avalonia.Windows.Tools;

/// <summary>
/// AOT 异步任务执行助手
/// 显式包装异步任务，避免“火与遗忘”导致的裁剪
/// </summary>
internal static class AotAsyncTaskTool
{
    /// <summary>
    /// 安全执行异步方法（AOT 友好）
    /// </summary>
    /// <param name="asyncFunc">异步方法委托</param>
    /// <param name="obj">参数</param>
    /// <param name="continueOnCapturedContext">是否延续捕获的上下文</param>
    /// <param name="logger">logger</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void RunSafeAsync<T>(Func<T?, Task> asyncFunc, T? obj, bool continueOnCapturedContext = false, ILogger? logger = default)
    {
        // 显式配置上下文，同时让修剪器识别到委托调用
        _ = ExecuteAsync(asyncFunc, obj, continueOnCapturedContext, logger);
    }

    /// <summary>
    /// 带返回值的异步方法安全执行
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void RunSafeAsync<T>(Func<Task<T>> asyncFunc, bool continueOnCapturedContext = false, ILogger? logger = default)
    {
        _ = ExecuteAsync(asyncFunc, continueOnCapturedContext, logger);
    }

    // 内部执行方法，避免顶层异步方法警告
    private static async Task ExecuteAsync<T>(Func<T?, Task> asyncFunc, T? obj, bool continueOnCapturedContext, ILogger? logger = default)
    {
        try
        {
            await asyncFunc(obj).ConfigureAwait(continueOnCapturedContext);
        }
        catch (Exception ex)
        {
            // 可自定义异常日志逻辑
            logger?.LogError($"AOT RunSafeAsync is fail: {ex.Message}；{ex.StackTrace}");
        }
    }

    private static async Task<T> ExecuteAsync<T>(Func<Task<T>> asyncFunc, bool continueOnCapturedContext, ILogger? logger = default)
    {
        try
        {
            return await asyncFunc().ConfigureAwait(continueOnCapturedContext);
        }
        catch (Exception ex)
        {
            logger?.LogError($"AOT RunSafeAsync is fail: {ex.Message}；{ex.StackTrace}");

            return default!;
        }
    }
}