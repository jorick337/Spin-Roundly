using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace MyTools.LocalAddressables
{
    public class LocalAssetLoader
    {
        private List<Func<UniTask>> _onUnloaded = new();

        private GameObject _cachedObject;
        private AsyncOperationHandle _handle;

        public async UniTask UnloadAsync()
        {
            await InvokeOnUnloaded();
            UnloadGameObject();
            UnloadObject();
        }

        #region GAME OBJECT

        protected async UniTask<T> LoadGameObjectAsync<T>(string assetsId, Transform transform = null)
        {
            _cachedObject = await Addressables.InstantiateAsync(assetsId, transform).Task;
            T prefab = _cachedObject.GetComponent<T>();

            return prefab;
        }

        protected void UnloadGameObject()
        {
            if (_cachedObject == null)
                return;

            _cachedObject.SetActive(false);
            Addressables.ReleaseInstance(_cachedObject);
            _cachedObject = null;
        }

        #endregion

        #region OBJECT

        protected async UniTask<T> LoadObjectAsync<T>(string assetsId)
        {
            _handle = Addressables.LoadAssetAsync<T>(assetsId);
            await _handle.Task;

            return (T)_handle.Result;
        }

        protected void UnloadObject()
        {
            if (!_handle.IsValid())
                return;

            Addressables.Release(_handle);
        }

        #endregion

        protected void AddEvent(Func<UniTask> action) => _onUnloaded.Add(action);

        protected async UniTask InvokeOnUnloaded() => await UniTask.WhenAll(_onUnloaded.Select(handler => handler()));
    }
}