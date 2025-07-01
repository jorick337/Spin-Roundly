using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using MyTools.Enemy;
using MyTools.UI;
using UnityEngine;

namespace MyTools.Levels.Play.Objects
{
    public class Enemy : LevelItem
    {
        #region CORE

        [Header("Enemy")]
        [SerializeField] private int _hitsToBreak;
        [SerializeField] private AudioSource _deathSound;

        [Header("Reward")]
        [SerializeField] private Coin[] _coins;
        [SerializeField] private Trophy[] _trophies;
        [SerializeField] private Teleport _teleportReward;

        [Header("Jump")]
        [SerializeField] private PatrolArea _patrolArea;
        [SerializeField] private ParticleSystem _particleSystem;

        [Header("Restart")]
        [SerializeField] private Teleport _teleportEnemy;
        [SerializeField] private Collider2D _collider2D;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private int _currentHits = 0;
        private bool _isDeath = false;

        #endregion

        #region MONO

        protected override void DoActionOnAwake() { }

        protected override void DoActionBeforeRestart()
        {
            _currentHits = 0;
            _isDeath = false;
            _teleportEnemy.SendToTarget();
            ActiveUI(true);
        }

        #endregion

        #region CORE LOGIC

        private void Death()
        {
            _isDeath = true;
            _teleportReward.SendToTarget();
            PlayBreakSound();
            ActiveUI(false);
        }

        private async UniTask ReboundAsync()
        {
            _patrolArea.enabled = false;
            await Stun();
            _patrolArea.enabled = true;
        }

        private async UniTask Stun()
        {
            _particleSystem.Stop();
            _particleSystem.Play();
            await UniTask.WaitUntil(() => _isDeath || !_particleSystem.isPlaying);
        }

        #endregion

        #region UI

        private void ActiveUI(bool active)
        {
            SetActiveCoins(!active);
            SetActiveTrophies(!active);
            _collider2D.enabled = active;
            _spriteRenderer.enabled = active;
            _patrolArea.enabled = active;
        }

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

        private void SetActiveTrophies(bool active)
        {
            for (int i = 0; i < _trophies.Length; i++)
            {
                if (active)
                    _trophies[i].Enable();
                else
                    _trophies[i].Disable();
            }
        }

        private void PlayBreakSound() => _deathSound.Play();

        #endregion

        #region CALLBACKS

        protected override async void InvokeTriggeredEnter(Collider2D collider2D)
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

        protected override void InvokeTriggeredStayAsync(Collider2D collider2D) { }
        protected override void InvokeTriggeredExit(Collider2D collider2D) { }

        #endregion
    }
}