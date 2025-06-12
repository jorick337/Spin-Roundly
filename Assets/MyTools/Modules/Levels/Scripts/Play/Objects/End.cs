using Cysharp.Threading.Tasks;
using MyTools.UI;
using UnityEngine;

namespace MyTools.Levels.Play
{
    public class End : MonoBehaviour
    {
        [SerializeField] private Collider2DTrigger _colliderTrigger;
        [SerializeField] private bool _isFinish = true;
        [SerializeField] private float _timeToWait;
        
        // Managers
        private GameLevelManager _gameLevelManager;

        private void Awake() => _gameLevelManager = GameLevelManager.Instance;
        private void OnEnable() => _colliderTrigger.OnTriggered += Apply;
        private void OnDisable() => _colliderTrigger.OnTriggered -= Apply;

        private async void Apply(Collider2D collider2D) 
        {
            await UniTask.WaitForSeconds(_timeToWait);

            if (_isFinish)
                _gameLevelManager.Finish();
            else
                _gameLevelManager.Restart();
        } 
    }
}