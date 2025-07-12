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

        protected override void Restart()
        {
            base.Restart();

            _currentHits = 0;
            DisableCoins();
            SetNormalSprite();
        }

        private void Break()
        {
            PlayBreakSound();
            EnableCoins();
            SetBrokenSprite();
        }

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

        protected override void Enter(Collider2D collider2D)
        {
            if (_currentHits >= _hitsToBreak)
                return;

            _currentHits++;
            if (_currentHits >= _hitsToBreak)
                Break();
        }
    }
}