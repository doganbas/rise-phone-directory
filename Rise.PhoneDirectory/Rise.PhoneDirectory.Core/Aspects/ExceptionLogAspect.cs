using Castle.DynamicProxy;
using Microsoft.Extensions.Logging;
using Rise.PhoneDirectory.Core.Interceptors;
using Rise.PhoneDirectory.Core.Tools;

namespace Rise.PhoneDirectory.Core.Aspects
{
    public class ExceptionLogAspect : MethodInterception
    {
        private ILogger _logger;

        public ExceptionLogAspect()
        {
            _logger = ServiceTool.GetService<ILogger>();
        }

        protected override void OnException(IInvocation invocation, Exception e)
        {
            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}");
            var arguments = invocation.Arguments.ToList();
            var messageContent = $"{methodName}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))})";
            _logger.LogError(e, messageContent);
        }
    }
}
