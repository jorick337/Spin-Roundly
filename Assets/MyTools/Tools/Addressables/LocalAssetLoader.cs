using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace MyTools.LocalAddressables
{
    public class LocalAssetLoader
    {
        private List<Func<UniTask>> _onUnloaded = new();

        private GameObject _cachedObject;

        public async UniTask UnloadAsync()
        {
            await InvokeOnUnloaded();
            UnloadInternal();
        }

        protected async UniTask<T> LoadInternal<T>(string assetsId, Transform transform = null)
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

        protected void AddEvent(Func<UniTask> action) => _onUnloaded.Add(action);

        protected async UniTask InvokeOnUnloaded() => await UniTask.WhenAll(_onUnloaded.Select(handler => handler()));
    }
}