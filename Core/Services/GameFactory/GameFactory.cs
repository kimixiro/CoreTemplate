using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Services.ResourceLoader;
using ShooterKitchen;
using UnityEngine;

namespace Core.Services.GameFactory
{
    public class GameFactory: IGameFactory
    {
        private readonly IResourceLoader _resourceLoader;
        
        public Player _cachedPlayer { get; private set; }

        public GameFactory(IResourceLoader resourceLoader)
        {
            _resourceLoader = resourceLoader;
        }

        public async Task<Player> CreatePlayer(Vector2 at)
        {
            if (_cachedPlayer != null)
            {
                return _cachedPlayer;
            }

            GameObject playerObject = await _resourceLoader.LoadAsync<GameObject>("Prefabs/Player");
            if (playerObject == null)
            {
                Debug.LogError("Failed to load the player prefab.");
                return null;
            }

            GameObject playerInstance = Object.Instantiate(playerObject, at, Quaternion.identity);
            Player player = playerInstance.GetComponent<Player>();
            if (player == null)
            {
                Debug.LogError("Player component is not attached to the player prefab.");
                Object.Destroy(playerInstance);
                return null;
            }

            _cachedPlayer = player;
            return player;
        }

    }
}