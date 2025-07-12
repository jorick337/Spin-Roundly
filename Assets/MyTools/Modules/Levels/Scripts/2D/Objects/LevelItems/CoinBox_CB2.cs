using Cysharp.Threading.Tasks;
using MyTools.Levels.Play;
using MyTools.UI;
using UnityEngine;

namespace MyTools.Levels.TwoDimensional.Objects
{
    public class CoinBox_CB2 : LevelItem
    {
        [Header("CoinBox")]
        [SerializeField] private Coin[] _coins;
        [SerializeField] private float _timeToDisableCollider = 0.1f;

        protected override async void Enter(Collider2D collider2D)
        {
            await WaitAfterDisable();
            DisableColliderTrigger();
            EnableCoins();
        }

        protected override void Restart()
        {
            base.Restart();
            DisableCoins();
        }

        private void EnableCoins()
        {
            for (int i = 0; i < _coins.Length; i++)
                _coins[i].Enable();
        }

        private void DisableCoins()
        {
            for (int i = 0; i < _coins.Length; i++)
                _coins[i].Disable();
        }

        private async UniTask WaitAfterDisable() => await UniTask.WaitForSeconds(_timeToDisableCollider);
    }
}