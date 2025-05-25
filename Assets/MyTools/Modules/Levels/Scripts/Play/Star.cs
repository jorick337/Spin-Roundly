using MyTools.UI;
using UnityEngine;

namespace MyTools.Levels.Play
{
    public class Star : MapItem
    {
        [Header("Managers")]
        [SerializeField] private GameLevel _gameLevel;

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
            _gameLevel.AddStar();
            _particleSystem.Play();
        }
    }
}