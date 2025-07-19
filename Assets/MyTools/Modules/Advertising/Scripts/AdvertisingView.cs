using Cysharp.Threading.Tasks;
using MyTools.Levels.Play;
using MyTools.UI.Objects.Buttons;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace MyTools.Advertising
{
    public class AdvertisingView : BaseButton
    {
        [Header("Advertising")]
        [SerializeField] private Slider _slider;
        [SerializeField] private float _time;

        private AdvertisingViewProvider _provider;
        private float _currentTime = 0;
        private bool _canGiveReward = false;
        private string _idAd = "1234";

        public async UniTask<bool> StartLoading()
        {
            while (_currentTime < _time && !_canGiveReward)
            {
                _slider.value = _currentTime / _time;
                _currentTime += Time.deltaTime;

                await UniTask.Yield();
            }

            DestroySelf();

            return _canGiveReward;
        }

        protected override void OnButtonPressed() => YG2.RewardedAdvShow(_idAd, SetTrueCanGiveReward);

        public void SetProvider(AdvertisingViewProvider provider) => _provider = provider;

        private void SetTrueCanGiveReward() => _canGiveReward = true;
        private async void DestroySelf() => await _provider.UnloadAllAsync();
    }
}