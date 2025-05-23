using MyTools.UI;
using UnityEngine;

namespace MyTools.Levels.Play
{
    public class Star : MonoBehaviour
    {
        [Header("Core")]
        [SerializeField] private SpriteRenderer _spriteRenderer; 
        [SerializeField] private ColliderTrigger _colliderTrigger;

        [Header("Animations")]
        [SerializeField] private ParticleSystem _particleSystem;

        // Managers
        private GameLevelManager _gameLevelManager;

        private void Awake() => _gameLevelManager = GameLevelManager.Instance;
        private void OnEnable() => _colliderTrigger.OnTriggered += Collect;
        private void OnDisable() => _colliderTrigger.OnTriggered -= Collect;

        private void Collect()
        {
            _gameLevelManager.AddStar();
            _particleSystem.Play();
        }
    }
}