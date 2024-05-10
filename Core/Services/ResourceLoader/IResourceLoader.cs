using System.Threading.Tasks;
using Core.Services.ServiceLocator;
using UnityEngine;

namespace Core.Services.ResourceLoader
{
    public interface IResourceLoader: IService
    {
        Task<T> LoadAsync<T>(string assetPath) where T : Object;
        T Load<T>(string assetPath) where T : Object;
    }
}