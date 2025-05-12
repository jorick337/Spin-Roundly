using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace MyTools.LocalAddressables
{
    public class LocalAssetLoader
    {
        private GameObject _cachedObject;

        protected async Task<T> LoadInternal<T>(string assetsId, Transform transform)
        {
            _cachedObject = await Addressables.InstantiateAsync(assetsId, transform).Task;
            T prefab = _cachedObject.GetComponent<T>();

            return prefab;
        }

        protected void UnloadInternal()
        {
            if (_cachedObject == null)
            {
                return;
            }

            _cachedObject.SetActive(false);
            Addressables.ReleaseInstance(_cachedObject);
            _cachedObject = null;
        }
    }
}