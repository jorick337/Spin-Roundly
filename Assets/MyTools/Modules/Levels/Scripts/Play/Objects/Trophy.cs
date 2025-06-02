using MyTools.PlayerSystem;
using MyTools.UI;
using UnityEngine;

namespace MyTools.Levels.Play
{
    public class Trophy : LevelItem
    {
        [Header("Trophy")]
        [SerializeField] private ParticleSystem _particleSystem;

        // Managers
        private PlayerManager _playerManager;

        protected override void DoActionOnAwake() => _playerManager = PlayerManager.Instance;
        protected override void DoActionBeforeRestart() { }

        protected override void InvokeTrigger2D(Collider2D collider2D)
        {
            _playerManager.Player.AddTrophy(1);
            _particleSystem.Play();
        }
    }
}