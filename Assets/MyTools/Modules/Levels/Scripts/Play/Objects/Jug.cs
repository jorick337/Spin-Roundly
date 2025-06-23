using MyTools.UI;
using UnityEngine;

namespace MyTools.Levels.Play
{
    public class Jug : LevelItem
    {
        #region CORE

        [Header("Jug")]
        [SerializeField] private int _hitsToBreak;
        [SerializeField] private AudioSource _breakSound;
        [SerializeField] private Coin[] _coins;

        [Header("Restart")]
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Sprite _normalSprite;
        [SerializeField] private Sprite _brokenSprite;

        private int _currentHits = 0;

        #endregion

        #region MONO

        protected override void DoActionOnAwake() { }

        protected override void DoActionBeforeRestart()
        {
            _currentHits = 0;
            DisableCoins();
            SetNormalSprite();
        }

        #endregion

        #region CORE LOGIC

        private void Break()
        {
            PlayBreakSound();
            EnableCoins();
            SetBrokenSprite();
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

        private void SetBrokenSprite() => _spriteRenderer.sprite = _brokenSprite;
        private void SetNormalSprite() => _spriteRenderer.sprite = _normalSprite;

        private void PlayBreakSound() => _breakSound.Play();

        #endregion

        #region CALLBACKS

        protected override void InvokeTriggeredEnter(Collider2D collider2D)
        {
            if (_currentHits >= _hitsToBreak)
                return;

            _currentHits++;
            if (_currentHits >= _hitsToBreak)
                Break();
        }

        protected override void InvokeTriggeredStay(Collider2D collider2D) { }
        protected override void InvokeTriggeredExit(Collider2D collider2D) { }

        #endregion
    }
}