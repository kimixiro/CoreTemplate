using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Services.ServiceLocator;
using ShooterKitchen;
using UnityEngine;

namespace Core.Services.GameFactory
{
    public interface IGameFactory: IService
    {
        public Player _cachedPlayer { get; }
        public Task<Player> CreatePlayer(Vector2 at);

    }
}