using Castle.DynamicProxy;
using Rise.PhoneDirectory.Core.Caching;
using Rise.PhoneDirectory.Core.Interceptors;
using Rise.PhoneDirectory.Core.Tools;

namespace Rise.PhoneDirectory.Core.Aspects
{
    public class CacheRemoveAspect : MethodInterception
    {
        private string _pattern;
        private ICacheManager _cacheManager;

        public CacheRemoveAspect(string pattern = null)
        {
            _cacheManager = ServiceTool.GetService<ICacheManager>();
            _pattern = pattern;
        }

        protected override void OnSuccess(IInvocation invocation)
        {
            if (_pattern == null)
                _pattern = string.Format($"{invocation.Method.ReflectedType.FullName}");
            _cacheManager.RemoveByPattern(_pattern);
        }
    }
}