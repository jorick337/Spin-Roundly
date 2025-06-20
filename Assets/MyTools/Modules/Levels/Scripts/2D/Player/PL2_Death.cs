using Cysharp.Threading.Tasks;
using MyTools.Advertising;
using UnityEngine;

namespace MyTools.Levels.TwoDimensional.Player
{
    public class PL2_Death : MonoBehaviour
    {
        [SerializeField] private Player_PL2 _player;

        private AdvertisingView _advertisingView;

        private void OnEnable() => _player.OnDead += Dead;
        private void OnDisable() => _player.OnDead -= Dead;

        private async UniTask Dead()
        {
            await LoadAdvertisingViewAsync();
            await TryReviveWithAd();
        }

        private async UniTask LoadAdvertisingViewAsync()
        {
            AdvertisingViewProvider provider = new();
            _advertisingView = await provider.LoadAsync();
        }

        private async UniTask TryReviveWithAd()
        {
            if (await _advertisingView.StartLoading())
                _player.Rebirth();
            else
                await _player.RestartAsync();
        }
    }
}