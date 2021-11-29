namespace CodeBase.Infrastructure.Services
{
    public class AllServices
    {
        private static AllServices instance;
        public static AllServices Container => instance ??= new AllServices();

        public void Register<TService>(TService service) where TService : IService
        {
            Implementation<TService>.ServiceInstance = service;
        }

        public TService Single<TService>() where TService : IService
        {
            return Implementation<TService>.ServiceInstance;
        }

        private static class Implementation<TService> where TService : IService
        {
            public static TService ServiceInstance;
        }
    }
}