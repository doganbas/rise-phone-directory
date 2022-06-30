using Castle.DynamicProxy;
using FluentValidation;
using Rise.PhoneDirectory.Core.Constants;
using Rise.PhoneDirectory.Core.Interceptors;
using Rise.PhoneDirectory.Core.Tools;

namespace Rise.PhoneDirectory.Core.Aspects
{
    public class ValidationAspect : MethodInterception
    {
        private readonly Type _validatorType;

        public ValidationAspect(Type validatorType)
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
                throw new Exception(ProjectConst.WrongValidationType);

            _validatorType = validatorType;
        }

        protected override void OnBefore(IInvocation invocation)
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType);
            var dataType = _validatorType.BaseType.GetGenericArguments()[0];
            var datas = invocation.Arguments.Where(nq => nq.GetType() == dataType);
            if (datas == null)
                return;
            foreach (var item in datas)
            {
                ValidationTool.Validate(validator, item);
            }
        }
    }
}