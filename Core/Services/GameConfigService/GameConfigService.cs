using Core.Services.ResourceLoader;
using Data;
using UnityEngine;

namespace Core.Services.GameConfigService
{
    public class GameConfigService: IGameConfigService
    {
        private readonly IResourceLoader _resourceLoader;
        public WorldConfig WorldConfig { get; private set; }

        public GameConfigService(IResourceLoader resourceLoader)
        {
            _resourceLoader = resourceLoader;
            LoadConfigAsync();
        }

        private async void LoadConfigAsync()
        {
            try
            {
                WorldConfigHolder worldConfigHolder = await _resourceLoader.LoadAsync<WorldConfigHolder>("Data/WorldConfig");
                if (worldConfigHolder != null && worldConfigHolder.Config != null)
                {
                    WorldConfig = worldConfigHolder.Config;
                }
                else
                {
                    Debug.LogError("Failed to load or parse the WorldConfig data.");
                    WorldConfig = new WorldConfig(); // Load default config or handle the error appropriately.
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"An error occurred while loading the WorldConfig: {ex.Message}");
                WorldConfig = new WorldConfig(); // Fallback to default configuration.
            }
        }
    }
}