using Cysharp.Threading.Tasks;
using MyTools.Advertising;
using MyTools.Levels.TwoDimensional.Player;
using UnityEngine;

namespace MyTools.Levels.TwoDimensional.Objects.Health
{
    public class HL2_Dead : MonoBehaviour
    {
        [SerializeField] private Health_HL2 _health;
        [SerializeField] private Player_PL2 _player;

        private AdvertisingView _advertisingView;
 
        private void OnEnable() => _health.OnBeforeDead += Apply;
        private void OnDisable() => _health.OnBeforeDead -= Apply;

        private async UniTask LoadAdvertisingViewAsync()
        {
            AdvertisingViewProvider provider = new();
            _advertisingView = await provider.LoadAsync();
        }

        private async UniTask TryReviveWithAd()
        {
            if (await _advertisingView.StartLoading())
                _health.Restart();
            else
                _player.Restart();
        }

        private async UniTask Apply() 
        {
            _player.Disable();
            await LoadAdvertisingViewAsync();
            await TryReviveWithAd();
            _player.Enable();
        }
    }
}