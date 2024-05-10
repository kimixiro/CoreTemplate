using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Core.Services.ResourceLoader
{
    public class ResourceLoader : IResourceLoader
    {
        private readonly Dictionary<string, Task<Object>> _loadingTasks = new();

        public async Task<T> LoadAsync<T>(string assetPath) where T : Object
        {
            if (!_loadingTasks.TryGetValue(assetPath, out Task<Object> task))
            {
                task = LoadResourceAsync<T>(assetPath);
                _loadingTasks[assetPath] = task;
            }
            
            try
            {
                return await task as T;
            }
            catch (System.InvalidCastException)
            {
                Debug.LogError($"Failed to load asset as {typeof(T).Name} at {assetPath}");
                return null;
            }
        }

        private async Task<Object> LoadResourceAsync<T>(string assetPath) where T : Object
        {
            ResourceRequest request = Resources.LoadAsync<T>(assetPath);
            await request;

            if (request.asset == null)
            {
                Debug.LogError($"Failed to load asset at {assetPath}");
                return null;
            }
            
            return request.asset;
        }

        public T Load<T>(string assetPath) where T : Object
        {
            if (!_loadingTasks.TryGetValue(assetPath, out Task<Object> task) || task.IsCompleted)
            {
                T asset = Resources.Load<T>(assetPath);
                if (asset == null)
                {
                    Debug.LogError($"Failed to load asset at {assetPath}");
                    return null;
                }

                _loadingTasks[assetPath] = Task.FromResult<Object>(asset);
                return asset;
            }

            return task.Result as T;
        }
    }
}