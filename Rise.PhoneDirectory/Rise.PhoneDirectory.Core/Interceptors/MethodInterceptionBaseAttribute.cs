using Castle.DynamicProxy;

namespace Rise.PhoneDirectory.Core.Interceptors
{
    public class MethodInterceptionBaseAttribute : Attribute, IInterceptor
    {
        public int Priority { get; set; }

        public virtual void Intercept(IInvocation invocation)
        {

        }
    }
}
