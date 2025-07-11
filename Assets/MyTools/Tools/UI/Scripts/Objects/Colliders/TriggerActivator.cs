using UnityEngine;

namespace MyTools.UI.Colliders
{
    public class TriggerActivator : MonoBehaviour
    {
        [Header("Activator")]
        [SerializeField] private ColliderTrigger2D _colliderTrigger2D;

        protected virtual void OnEnable()
        {
            _colliderTrigger2D.OnTriggeredEnter += Enter;
            _colliderTrigger2D.OnTriggeredStay += Stay;
            _colliderTrigger2D.OnTriggeredExit += Exit;
        }

        protected virtual void OnDisable()
        {
            _colliderTrigger2D.OnTriggeredEnter -= Enter;
            _colliderTrigger2D.OnTriggeredStay -= Stay;
            _colliderTrigger2D.OnTriggeredExit -= Exit;
        }

        protected virtual void OnValidate()
        {
            if (_colliderTrigger2D == null)
                _colliderTrigger2D = GetComponent<ColliderTrigger2D>();
        }

        protected virtual void Enter(Collider2D collider2D) { }
        protected virtual void Stay(Collider2D collider2D) { }
        protected virtual void Exit(Collider2D collider2D) { }
    }
}