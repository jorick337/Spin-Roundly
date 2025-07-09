using MyTools.Levels.Play;
using MyTools.UI.Colliders;
using UnityEngine;

namespace MyTools.UI
{
    public abstract class LevelItem : MonoBehaviour
    {
        #region CORE

        [Header("Core")]
        [SerializeField] protected ColliderTrigger2D _collider2DTrigger;

        protected GameLevelManager _gameLevelManager;

        #endregion

        #region MONO

        protected abstract void DoActionOnAwake();
        protected abstract void DoActionBeforeRestart();

        private void Awake()
        {
            _gameLevelManager = GameLevelManager.Instance;
            DoActionOnAwake();
        }

        private void OnEnable()
        {
            _collider2DTrigger.OnTriggeredEnter += InvokeTriggeredEnter;
            _collider2DTrigger.OnTriggeredStay += InvokeTriggeredStay;
            _collider2DTrigger.OnTriggeredExit += InvokeTriggeredExit;
            _gameLevelManager.OnRestart += Restart;
        }

        private void OnDisable()
        {
            _collider2DTrigger.OnTriggeredEnter -= InvokeTriggeredEnter;
            _collider2DTrigger.OnTriggeredStay -= InvokeTriggeredStay;
            _collider2DTrigger.OnTriggeredExit -= InvokeTriggeredExit;
            _gameLevelManager.OnRestart -= Restart;
        }

        #endregion

        #region UI

        public void Enable()
        {
            EnableLevelItem();
            EnableColliderTrigger();
        }

        public void Disable()
        {
            DisableLevelItem();
            DisableColliderTrigger();
        }

        protected void EnableLevelItem() => gameObject.SetActive(true);
        protected void DisableLevelItem() => gameObject.SetActive(false);

        protected void EnableColliderTrigger() => _collider2DTrigger.Enable();
        protected void DisableColliderTrigger() => _collider2DTrigger.Disable();

        #endregion

        #region CALLBACKS

        protected void Restart()
        {
            DoActionBeforeRestart();
            EnableColliderTrigger();
        }

        protected abstract void InvokeTriggeredEnter(Collider2D collider2D);
        protected abstract void InvokeTriggeredStay(Collider2D collider2D);
        protected abstract void InvokeTriggeredExit(Collider2D collider2D);

        #endregion
    }
}