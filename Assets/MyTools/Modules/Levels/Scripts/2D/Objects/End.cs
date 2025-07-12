using MyTools.UI.Colliders;
using UnityEngine;

namespace MyTools.Levels.Play
{
    public class End : MonoBehaviour
    {
        [SerializeField] private ColliderTrigger2D _colliderTrigger;
        [SerializeField] private bool _isFinish = true;

        // Managers
        private GameLevelManager _gameLevelManager;

        private void Awake() => _gameLevelManager = GameLevelManager.Instance;

        private void OnEnable()
        {
            _gameLevelManager.OnRestart += Restart;
            _colliderTrigger.OnTriggeredEnter += Apply;
        }

        private void OnDisable()
        {
            _gameLevelManager.OnRestart -= Restart;
            _colliderTrigger.OnTriggeredEnter -= Apply;
        }

        private void Apply(Collider2D collider2D)
        {
            if (_isFinish)
                _gameLevelManager.Finish();
            else
                _gameLevelManager.RestartAsync();
        }

        private void Restart() => _colliderTrigger.Enable();
    }
}