namespace Core.Services.ServiceLocator
{
    public interface IAllServices
    {
        void RegisterSingle<TService>(TService implementation) where TService : IService;
        TService Single<TService>() where TService : IService;
    }
}