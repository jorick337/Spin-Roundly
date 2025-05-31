using UnityEngine;
using UnityEngine.Events;

namespace MyTools.UI
{
    public class Collider2DTrigger : MonoBehaviour
    {
        #region EVENTS

        public UnityAction<Collider2D> OnTriggered;

        #endregion

        #region CORE

        [Header("Core")]
        [SerializeField] private bool _repeatable = false;
        [SerializeField] private bool _allowActivation = true;
        [SerializeField] private string _tag = "Player";

        [Header("Jump")]
        [SerializeField] private bool _onlyTriggerFromAbove = false;
        [SerializeField] private float _minJumpHeight = 0;

        #endregion

        #region MONO

        private void OnTriggerEnter2D(Collider2D collider2D)
        {
            if (!collider2D.transform.CompareTag(_tag))
                return;

            if (_onlyTriggerFromAbove && !IsTriggerFromAbove(collider2D))
                return;
                
            InvokeOnTriggered(collider2D);

            if (_allowActivation)
                SetActiveByLooping();
        }

        #endregion

        #region UI

        public void Enable() => gameObject.SetActive(true);
        public void Disable() => gameObject.SetActive(false);

        #endregion

        #region VALUES

        private void SetActiveByLooping() => gameObject.SetActive(_repeatable);

        protected bool IsTriggerFromAbove(Collider2D collider2D)
        {
            float triggerY = collider2D.transform.position.y;
            float transformY = transform.position.y;

            return triggerY > transformY + _minJumpHeight;
        }

        #endregion

        #region CALLBACKS

        private void InvokeOnTriggered(Collider2D collider2D) => OnTriggered?.Invoke(collider2D);

        #endregion
    }
}