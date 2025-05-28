using MyTools.Levels.Play;
using UnityEngine;

namespace MyTools.UI
{
    public abstract class MapItem : MonoBehaviour
    {
        [Header("Core")]
        [SerializeField] protected ColliderTrigger _colliderTrigger;
        [SerializeField] protected GameLevel _gameLevel;

        protected abstract void ActivateTriggerEnter2D();
        protected abstract void ActivateCollisionEnter2D(Collision2D collision2D);

        protected abstract void DoActionBeforeRestart();

        private void OnEnable()
        {
            _colliderTrigger.TriggerEnter2D += ActivateTriggerEnter2D;
            _colliderTrigger.CollisionEnter2D += ActivateCollisionEnter2D;
            _gameLevel.OnReload += Restart;
        }

        private void OnDisable()
        {
            _colliderTrigger.TriggerEnter2D -= ActivateTriggerEnter2D;
            _colliderTrigger.CollisionEnter2D -= ActivateCollisionEnter2D;
            _gameLevel.OnReload -= Restart;
        }

        public void Restart()
        {
            DoActionBeforeRestart();
            EnableColliderTrigger();
        }

        public void Enable()
        {
            gameObject.SetActive(true);
            EnableColliderTrigger();
        }

        public void Disable()
        {
            gameObject.SetActive(false);
            DisableColliderTrigger();
        }

        public void EnableColliderTrigger() => _colliderTrigger.Enable();
        public void DisableColliderTrigger() => _colliderTrigger.Disable();
    }
}