using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace MyTools.Advertising
{
    public class AdvertisingView : MonoBehaviour
    {
        [SerializeField] private float _time;
        [SerializeField] private Slider _slider;

        private float _currentTime;

        // Managers
        private AdvertisingViewProvider _provider;

        public async UniTask<bool> StartLoading()
        {
            Restart();

            while (_currentTime < _time)
            {
                _slider.value = _currentTime / _time;
                _currentTime += Time.deltaTime;

                await UniTask.Yield();
            }

            Finish();
            return true;
        }

        private void Restart()
        {
            _currentTime = 0;
            _slider.value = 0;
        }

        private void Finish()
        {
            _slider.value = 1f;
            DestroySelf();
        }

        public void SetProvider(AdvertisingViewProvider provider) => _provider = provider;

        private async void DestroySelf() => await _provider.UnloadAllAsync();
    }
}