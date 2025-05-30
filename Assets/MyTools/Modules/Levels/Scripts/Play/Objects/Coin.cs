using MyTools.PlayerSystem;
using MyTools.UI;
using UnityEngine;

namespace MyTools.Levels.Play
{
    public class Coin : MapItem
    {
        [Header("Coin")]
        [SerializeField] private ParticleSystem _particleSystem;

        private PlayerManager _playerManager;

        protected override void DoActionOnAwake() => _playerManager = PlayerManager.Instance;
        
        protected override void ActivateTriggerEnter2D()
        {
            _playerManager.Player.AddMoney(1);
            _particleSystem.Play();
        }

        protected override void ActivateCollisionEnter2D(Collision2D collision2D) { }

        protected override void DoActionBeforeRestart() { }


    }
}