using MyTools.PlayerSystem;
using MyTools.UI;
using UnityEngine;

namespace MyTools.Levels.Play
{
    public class Coin : MapItem
    {
        [Header("Managers")]
        [SerializeField] private GameLevel _gameLevel;

        private PlayerManager _playerManager;

        private void Awake() => _playerManager = PlayerManager.Instance;

        private void OnEnable()
        {
            _colliderTrigger.OnTriggered += Collect;
            _gameLevel.OnReload += EnableCollider;
        }

        private void OnDisable()
        {
            _colliderTrigger.OnTriggered -= Collect;
            _gameLevel.OnReload -= EnableCollider;
        }

        private void Collect()
        {
            _playerManager.Player.AddMoney(1);
            _particleSystem.Play();
        }
    }
}