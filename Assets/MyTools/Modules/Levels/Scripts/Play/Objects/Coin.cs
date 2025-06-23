using MyTools.PlayerSystem;
using MyTools.UI;
using UnityEngine;

namespace MyTools.Levels.Play
{
    public class Coin : LevelItem
    {
        [Header("Coin")]
        [SerializeField] private ParticleSystem _particleSystem;

        // Managers
        private PlayerManager _playerManager;

        protected override void DoActionOnAwake() => _playerManager = PlayerManager.Instance;
        protected override void DoActionBeforeRestart() { }

        protected override void InvokeTriggeredEnter(Collider2D collider2D)
        {
            _playerManager.Player.AddMoney(1);
            _particleSystem.Play();
        }

        protected override void InvokeTriggeredStay(Collider2D collider2D) { }

        protected override void InvokeTriggeredExit(Collider2D collider2D) { }
    }
}