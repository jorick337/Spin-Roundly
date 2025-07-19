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

        private GameObject _currentCachedObject;
        private GameObject _previousCachedObject;

        private AsyncOperationHandle _currentHandle;
        private AsyncOperationHandle _previousHandle;

        public async UniTask UnloadAllAsync()
        {
            await InvokeOnUnloaded();
            UnloadCurrentCachedObject();
            UnloadPreviousCachedObject();
            UnloadCurrentHandle();
            UnloadPreviousHandle();
        }

        #region GAME OBJECT

        protected async UniTask<T> LoadGameObjectAsync<T>(string assetsId, Transform transform = null)
        {
            _previousCachedObject = _currentCachedObject;
            _currentCachedObject = await Addressables.InstantiateAsync(assetsId, transform).Task;
            T prefab = _currentCachedObject.GetComponent<T>();

            return prefab;
        }

        protected void UnloadCurrentCachedObject()
        {
            if (_currentCachedObject == null)
                return;

            _currentCachedObject.SetActive(false);
            Addressables.ReleaseInstance(_currentCachedObject);
            _currentCachedObject = null;
        }

        protected void UnloadPreviousCachedObject()
        {
            if (_previousCachedObject == null)
                return;

            _previousCachedObject.SetActive(false);
            Addressables.ReleaseInstance(_previousCachedObject);
            _previousCachedObject = null;
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

        protected void UnloadCurrentHandle()
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