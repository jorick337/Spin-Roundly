using MyTools.Levels.Play;
using MyTools.UI.Colliders;

namespace MyTools.UI
{
    public abstract class LevelItem : TriggerActivator
    {
        protected GameLevelManager _gameLevelManager;

        protected virtual void Awake() => _gameLevelManager = GameLevelManager.Instance;

        protected override void OnEnable()
        {
            base.OnEnable();
            _gameLevelManager.OnRestart += Restart;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _gameLevelManager.OnRestart -= Restart;
        }

        #region UI

        public virtual void Enable()
        {
            EnableSelf();
            EnableColliderTrigger();
        }

        public virtual void Disable()
        {
            DisableSelf();
            DisableColliderTrigger();
        }

        protected void EnableSelf() => gameObject.SetActive(true);
        protected void DisableSelf() => gameObject.SetActive(false);

        protected void EnableColliderTrigger() => _colliderTrigger2D.Enable();
        protected void DisableColliderTrigger() => _colliderTrigger2D.Disable();

        #endregion

        protected virtual void Restart() => EnableColliderTrigger();
    }
}