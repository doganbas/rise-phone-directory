namespace Rise.PhoneDirectory.Core.Tools
{
    public static class ServiceTool
    {
        private static IServiceProvider ServiceCollections { get; set; }

        public static void Set(IServiceProvider services)
        {
            ServiceCollections = services;
        }

        public static TService GetService<TService>()
        {
            return (TService)ServiceCollections.GetService(typeof(TService));
        }
    }
}