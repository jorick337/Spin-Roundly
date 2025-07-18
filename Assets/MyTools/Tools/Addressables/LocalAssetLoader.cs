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

        private AsyncOperationHandle _currentHandle;
        private AsyncOperationHandle _previousHandle;

        public async UniTask UnloadAllAsync()
        {
            await InvokeOnUnloaded();
            UnloadGameObject();
            UnloadCurrentObject();
            UnloadPreviousHandle();
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
            _previousHandle = _currentHandle;
            _currentHandle = Addressables.LoadAssetAsync<T>(assetsId);
            await _currentHandle.Task;

            return (T)_currentHandle.Result;
        }

        protected void UnloadCurrentObject()
        {
            if (!_currentHandle.IsValid())
                return;

            Addressables.Release(_currentHandle);
        }

        protected void UnloadPreviousHandle()
        {
            if (!_previousHandle.IsValid())
                return;

            Addressables.Release(_previousHandle);
        }

        #endregion

        protected void AddEvent(Func<UniTask> action) => _onUnloaded.Add(action);

        protected async UniTask InvokeOnUnloaded() => await UniTask.WhenAll(_onUnloaded.Select(handler => handler()));
    }
}