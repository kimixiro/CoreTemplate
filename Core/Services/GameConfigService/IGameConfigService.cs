using Core.Services.ServiceLocator;
using Data;

namespace Core.Services.GameConfigService
{
    public interface IGameConfigService : IService
    {
        public WorldConfig WorldConfig { get; }
    }
}