using MyTools.UI;
using UnityEngine;

namespace MyTools.Levels.Play
{
    public class Star : MonoBehaviour
    {
        [Header("Core")]
        [SerializeField] private ColliderTrigger _colliderTrigger;
        [SerializeField] private ParticleSystem _particleSystem;

        [Header("Managers")]
        [SerializeField] private GameLevel _gameLevel;

        private GameLevelManager _gameLevelManager;

        private void Awake() => _gameLevelManager = GameLevelManager.Instance;

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
            _gameLevelManager.AddStar();
            _particleSystem.Play();
        }

        private void EnableCollider() => _colliderTrigger.Enable();
    }
}