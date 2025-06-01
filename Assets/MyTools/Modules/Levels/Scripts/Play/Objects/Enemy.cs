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
        [SerializeField] private Teleport _teleportCoins;

        [Header("Jump")]
        [SerializeField] private Movement2D _movement2D;
        [SerializeField] private PatrolArea _patrolArea;
        [SerializeField] private ParticleSystem _particleSystem;

        [Header("Restart")]
        [SerializeField] private Teleport _teleportEnemy;
        [SerializeField] private Collider2D _collider2D;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private int _currentHits = 0;

        #endregion

        #region MONO

        protected override void DoActionOnAwake() { }

        protected override void DoActionBeforeRestart()
        {
            _currentHits = 0;
            _teleportEnemy.SendToTarget();
            ActiveUI(true);
        }

        #endregion

        #region CORE LOGIC

        private void Death()
        {
            _teleportCoins.SendToTarget();
            PlayBreakSound();
            ActiveUI(false);
        }

        private async UniTask ReboundAsync()
        {
            _patrolArea.enabled = false;
            _movement2D.Jump();
            await Stun();
            _patrolArea.enabled = true;
        }

        private async UniTask Stun()
        {
            _particleSystem.Stop();
            _particleSystem.Play();
            await UniTask.WaitUntil(() => !_particleSystem.isPlaying);
        }

        #endregion

        #region UI

        private void ActiveUI(bool active)
        {
            _patrolArea.enabled = active;
            SetActiveCoins(!active);
            _collider2D.enabled = active;
            _spriteRenderer.enabled = active;
        }

        private void PlayBreakSound() => _deathSound.Play();

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

        #endregion

        #region CALLBACKS

        protected override async void InvokeTrigger2D(Collider2D collider2D)
        {
            if (_currentHits >= _hitsToBreak)
                return;

            _currentHits++;

            if (_currentHits >= _hitsToBreak)
            {
                Death();
                return;
            }

            await ReboundAsync();
        }

        #endregion
    }
}