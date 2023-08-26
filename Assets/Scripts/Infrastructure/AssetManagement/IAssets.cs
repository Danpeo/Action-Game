using System.Threading.Tasks;
using Infrastructure.Services;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Infrastructure.AssetManagement
{
    public interface IAssets : IService
    {
        Task<GameObject> Instantiate(string path);
        Task<GameObject> Instantiate(string path, Vector3 at);
        Task<T> Load<T>(AssetReference monsterDataPrefabReference) where T : class;
        Task<T> Load<T>(string address) where T : class;
        void Cleanup();
        void Initialize();
    }
}