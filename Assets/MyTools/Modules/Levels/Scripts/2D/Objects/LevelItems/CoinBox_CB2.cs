using MyTools.Levels.Play;
using MyTools.UI;
using UnityEngine;

namespace MyTools.Levels.TwoDimensional.Objects
{
    public class CoinBox_CB2 : LevelItem
    {
        [Header("MoneyBox")]
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private Coin[] _coins;

        protected override void DoActionBeforeRestart() => DisableCoins();
        protected override void DoActionOnAwake() { }

        protected override void InvokeTriggeredEnter(Collider2D collider2D) { }

        protected override void InvokeTriggeredExit(Collider2D collider2D)
        {
            _audioSource.Play();
            _particleSystem.Play();
            EnableCoins();
            DisableColliderTrigger();
        }

        protected override void InvokeTriggeredStayAsync(Collider2D collider2D) { }

        private void DisableCoins()
        {
            for (int i = 0; i < _coins.Length; i++)
                _coins[i].Disable();
        }

        private void EnableCoins()
        {
            for (int i = 0; i < _coins.Length; i++)
                _coins[i].Enable();
        }
    }
}