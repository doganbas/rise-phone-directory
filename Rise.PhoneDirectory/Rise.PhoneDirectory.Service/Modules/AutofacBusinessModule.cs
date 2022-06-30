using Autofac;
using Autofac.Builder;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using Rise.PhoneDirectory.Core.Interceptors;
using Rise.PhoneDirectory.Core.Repositories;
using Rise.PhoneDirectory.Core.Services;
using Rise.PhoneDirectory.Core.UnitOfWorks;
using Rise.PhoneDirectory.Repository;
using Rise.PhoneDirectory.Repository.Repositories;
using Rise.PhoneDirectory.Repository.UnitOfWorks;
using Rise.PhoneDirectory.Service.Mappings;
using Rise.PhoneDirectory.Service.Services;
using System.Reflection;
using Module = Autofac.Module;

namespace Rise.PhoneDirectory.Service.Modules
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).RegisterInterceptors().InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(GenericService<>)).As(typeof(IGenericService<>)).RegisterInterceptors().InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();

            var apiAssembly = Assembly.GetExecutingAssembly();
            var repoAssembly = Assembly.GetAssembly(typeof(PhoneDirectoryDbContext));
            var serviceAssembly = Assembly.GetAssembly(typeof(MapProfile));

            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).Where(x => x.Name.EndsWith("Repository")).AsImplementedInterfaces().RegisterInterceptors().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).Where(x => x.Name.EndsWith("Service")).AsImplementedInterfaces().RegisterInterceptors().InstancePerLifetimeScope();

            base.Load(builder);
        }
    }

    public static class AutofacBusinessInterceptor
    {
        public static IRegistrationBuilder<TLimit, ReflectionActivatorData, DynamicRegistrationStyle> RegisterInterceptors<TLimit>(this IRegistrationBuilder<TLimit, ReflectionActivatorData, DynamicRegistrationStyle> registration)
        {
            if (registration == null)
                throw new ArgumentNullException("Autofac Registration Error");
            return registration.EnableInterfaceInterceptors(new ProxyGenerationOptions()
            {
                Selector = new AspectInterceptorSelector()
            });
        }
    }
}