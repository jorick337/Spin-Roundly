using System;
using Cysharp.Threading.Tasks;
using MyTools.Levels.Play;
using MyTools.Movement.TwoDimensional;
using MyTools.UI;
using UnityEngine;

namespace MyTools.Enemy
{
    public class Enemy : LevelItem
    {
        #region CORE

        [Header("Enemy")]
        [SerializeField] private int _hitsToBreak;
        [SerializeField] private AudioSource _deathSound;
        [SerializeField] private Coin[] _coins;

        [Header("Jump")]
        [SerializeField] private Movement2D _movement2D;
        [SerializeField] private PatrolArea _patrolArea;
        [SerializeField] private ParticleSystem _particleSystem;

        private int _currentHits = 0;

        #endregion

        #region MONO

        protected override void DoActionOnAwake() { }

        protected override void DoActionBeforeRestart()
        {
            _currentHits = 0;
            DisableCoins();
            Enable();
            _patrolArea.Enable();
        }

        #endregion

        #region CORE LOGIC

        private async void BreakAsync()
        {
            await PlayBreakSoundAsync();
            EnableCoins();
            Disable();
        }

        private async UniTask ReboundAsync()
        {
            _patrolArea.Disable();
            _movement2D.Jump();
            await Stun();
            _patrolArea.Enable();
        }

        private async UniTask Stun()
        {
            _particleSystem.Stop();
            _particleSystem.Play();
            await UniTask.WaitUntil(() => !_particleSystem.isPlaying);
        }

        #endregion

        #region UI

        private void SetActiveCoins(bool active)
        {
            for (int i = 0; i < _coins.Length; i++)
            {
                if (active)
                    _coins[i].Enable();
                else
                    _coins[i].Disable();
            }
        }

        private void EnableCoins() => SetActiveCoins(true);
        private void DisableCoins() => SetActiveCoins(false);

        private async UniTask PlayBreakSoundAsync()
        {
            _deathSound.Play();
            await UniTask.Delay(TimeSpan.FromSeconds(_deathSound.clip.length));
        }

        #endregion

        #region CALLBACKS

        protected override async void InvokeTrigger2D(Collider2D collider2D)
        {
            if (_currentHits >= _hitsToBreak)
                return;

            _currentHits++;

            if (_currentHits >= _hitsToBreak)
                BreakAsync();

            await ReboundAsync();
        }

        #endregion
    }
}