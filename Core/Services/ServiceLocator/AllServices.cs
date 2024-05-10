using System;
using System.Collections.Concurrent;

namespace Core.Services.ServiceLocator
{
    public class AllServices : IAllServices
    {
        private static readonly Lazy<AllServices> _instance = new Lazy<AllServices>(() => new AllServices());
        public static IAllServices Container => _instance.Value;

        private readonly ConcurrentDictionary<Type, IService> _services = new ConcurrentDictionary<Type, IService>();

        public void RegisterSingle<TService>(TService implementation) where TService : IService
        {
            _services[typeof(TService)] = implementation ?? throw new ArgumentNullException(nameof(implementation));
        }

        public TService Single<TService>() where TService : IService
        {
            if (_services.TryGetValue(typeof(TService), out IService service))
            {
                return (TService)service;
            }

            throw new InvalidOperationException($"No service registered for type {typeof(TService)}.");
        }
    }
}