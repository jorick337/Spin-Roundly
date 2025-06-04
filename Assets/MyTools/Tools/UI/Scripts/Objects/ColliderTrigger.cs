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

        [Header("Vertical")]
        [SerializeField] private bool _onlyTriggerFromVertical = false;
        [SerializeField] private float _verticalDistance = 0;

        [Header("Horizontal")]
        [SerializeField] private bool _onlyTriggerFromHorizontal = false;
        [SerializeField] private float _horizontalDistance = 0;

        #endregion

        #region MONO

        private void OnTriggerEnter2D(Collider2D collider2D)
        {
            if (!collider2D.transform.CompareTag(_tag))
                return;

            if (_onlyTriggerFromVertical && !IsTriggerFromVertical(collider2D))
                return;

            if (_onlyTriggerFromHorizontal && !IsTriggerFromHorizontal(collider2D))
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

        private bool IsTriggerFromVertical(Collider2D collider2D)
        {
            float triggerY = collider2D.transform.position.y;
            float transformY = transform.position.y;

            return triggerY > transformY + _verticalDistance;
        }

        private bool IsTriggerFromHorizontal(Collider2D collider2D)
        {
            float triggerX = collider2D.transform.position.x;
            float transformX = transform.position.x;

            return triggerX > transformX + _horizontalDistance;
        }

        private void SetActiveByLooping() => gameObject.SetActive(_repeatable);

        #endregion

        #region CALLBACKS

        private void InvokeOnTriggered(Collider2D collider2D) => OnTriggered?.Invoke(collider2D);

        #endregion
    }
}