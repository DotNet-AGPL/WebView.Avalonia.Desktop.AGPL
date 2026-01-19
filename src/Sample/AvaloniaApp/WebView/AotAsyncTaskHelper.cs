using System.Runtime.CompilerServices;

namespace AvaloniaApp
{
    /*
    /// <summary>
    /// AOT 异步方法防裁剪特性
    /// 自动保留目标类型的异步方法及隐式状态机元数据
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
    public sealed class AotAsyncMethodPreserveAttribute : DynamicDependencyAttribute
    {
        /// <summary>
        /// 保留指定类型的所有异步方法及状态机
        /// </summary>
        /// <param name="targetType">目标类型</param>
        public AotAsyncMethodPreserveAttribute(Type targetType)
            : base(
                DynamicallyAccessedMemberTypes.NonPublicMethods |
                DynamicallyAccessedMemberTypes.NonPublicConstructors |
                DynamicallyAccessedMemberTypes.NonPublicFields,
                targetType)
        {
        }

        /// <summary>
        /// 保留指定类型的指定异步方法
        /// </summary>
        /// <param name="methodName">异步方法名</param>
        /// <param name="targetType">目标类型</param>
        public AotAsyncMethodPreserveAttribute(string methodName, Type targetType)
            : base(
                DynamicallyAccessedMemberTypes.NonPublicMethods |
                DynamicallyAccessedMemberTypes.NonPublicConstructors |
                DynamicallyAccessedMemberTypes.NonPublicFields,
                methodName,
                targetType)
        {
        }
    }
    */

    /// <summary>
    /// AOT 异步任务执行助手
    /// 显式包装异步任务，避免“火与遗忘”导致的裁剪
    /// </summary>
    public static class AotAsyncTaskHelper
    {
        /// <summary>
        /// 安全执行异步方法（AOT 友好）
        /// </summary>
        /// <param name="asyncFunc">异步方法委托</param>
        /// <param name="continueOnCapturedContext">是否延续捕获的上下文</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RunSafeAsync(Func<Task> asyncFunc, bool continueOnCapturedContext = false)
        {
            // 显式配置上下文，同时让修剪器识别到委托调用
            _ = ExecuteAsync(asyncFunc, continueOnCapturedContext);
        }

        /// <summary>
        /// 带返回值的异步方法安全执行
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RunSafeAsync<T>(Func<Task<T>> asyncFunc, bool continueOnCapturedContext = false)
        {
            _ = ExecuteAsync(asyncFunc, continueOnCapturedContext);
        }

        // 内部执行方法，避免顶层异步方法警告
        private static async Task ExecuteAsync(Func<Task> asyncFunc, bool continueOnCapturedContext)
        {
            try
            {
                await asyncFunc().ConfigureAwait(continueOnCapturedContext);
            }
            catch (Exception ex)
            {
                // 可自定义异常日志逻辑
                Console.WriteLine($"AOT 异步方法执行失败: {ex.Message}");
            }
        }

        private static async Task<T> ExecuteAsync<T>(Func<Task<T>> asyncFunc, bool continueOnCapturedContext)
        {
            try
            {
                return await asyncFunc().ConfigureAwait(continueOnCapturedContext);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"AOT 异步方法执行失败: {ex.Message}");
                return default!;
            }
        }
    }
}