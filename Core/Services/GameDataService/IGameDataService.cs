using Core.Services.ServiceLocator;
using Data;

namespace Core.Services.GameDataService
{
    public interface IGameDataService: IService
    {
        GameData GameData { get; }
        void LoadGameData();
        void SaveGameData();
    }
}