using MyTools.UI;
using UnityEngine;

namespace MyTools.Levels.Play
{
    public class Jug : MapItem
    {
        [Header("Jug")]
        [SerializeField] private int _hitsToBreak;
        [SerializeField] private AudioSource _breakSound;
        [SerializeField] private Coin[] _coins;

        [Header("Restart")]
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Sprite _normalSprite;
        [SerializeField] private Sprite _brokenSprite;

        private int _currentHits = 0;

        protected override void DoActionOnAwake() { }

        protected override void ActivateCollisionEnter2D(Collision2D collision) { }

        protected override void ActivateTriggerEnter2D()
        {
            if (_currentHits >= _hitsToBreak)
                return;

            _currentHits++;
            if (_currentHits >= _hitsToBreak)
                Break();
        }

        protected override void DoActionBeforeRestart()
        {
            _currentHits = 0;
            EnableColliderTrigger();
            DisableCoins();
            SetNormalSprite();
        }

        private void Break()
        {
            PlayBreakSound();
            EnableCoins();
            SetBrokenSprite();
        }

        private void EnableCoins() => SetActiveCoins(true);
        private void DisableCoins() => SetActiveCoins(false);

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

        private void SetBrokenSprite() => _spriteRenderer.sprite = _brokenSprite;
        private void SetNormalSprite() => _spriteRenderer.sprite = _normalSprite;

        private void PlayBreakSound() => _breakSound.Play();
    }
}