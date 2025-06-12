using UnityEngine;
using UnityEngine.Events;

namespace MyTools.UI
{
    public class Collider2DTrigger : MonoBehaviour
    {
        #region EVENTS

        public UnityAction<Collider2D> OnTriggeredEnter;
        public UnityAction<Collider2D> OnTriggeredStay;
        public UnityAction<Collider2D> OnTriggeredExit;

        #endregion

        #region CORE

        [Header("Core")]
        [SerializeField] private bool _repeatable = false;
        [SerializeField] private bool _allowActivation = true;

        [Header("Tag")]
        [SerializeField] private bool _useTag = true;
        [SerializeField] private string _tag = "Player";

        [Header("Layer")]
        [SerializeField] private bool _useLayer = false;
        [SerializeField] private string _layer = "Default";

        [Header("Directions")]
        [SerializeField] private bool _canAll = false;
        [SerializeField] private bool _canTop = false;
        [SerializeField] private float _topDistance = 0;
        [SerializeField] private bool _canBottom = false;
        [SerializeField] private float _bottomDistance = 0;
        [SerializeField] private bool _canLeft = false;
        [SerializeField] private float _leftDistance = 0;
        [SerializeField] private bool _canRight = false;
        [SerializeField] private float _rightDistance = 0;

        public enum Direction { Top, Bottom, Left, Right }

        #endregion

        #region MONO

        private void OnTriggerEnter2D(Collider2D collider2D) => HandleTriggerEvent(collider2D, InvokeOnTriggeredEnter);
        private void OnTriggerStay2D(Collider2D collider2D) => HandleTriggerEvent(collider2D, InvokeOnTriggeredStay);
        private void OnTriggerExit2D(Collider2D collider2D) => HandleTriggerEvent(collider2D, InvokeOnTriggerdExit);

        #endregion

        #region UI

        public void Enable() => gameObject.SetActive(true);
        public void Disable() => gameObject.SetActive(false);

        #endregion

        #region VALUES

        private void HandleTriggerEvent(Collider2D collider2D, System.Action<Collider2D> callback)
        {
            if (!IsTriggerCorrectly(collider2D))
                return;

            callback?.Invoke(collider2D);

            if (_allowActivation)
                SetActiveByLooping();
        }

        private bool IsTriggerCorrectly(Collider2D collider2D)
        {
            if (_useTag && !collider2D.transform.CompareTag(_tag))
                return false;

            if (_useLayer && !(collider2D.gameObject.layer == LayerMask.NameToLayer(_layer)))
                return false;

            if (_canAll)
                return true;

            bool can = false;

            if (_canTop && IsTriggerFrom(Direction.Top, collider2D))
                can = true;
            if (_canLeft && IsTriggerFrom(Direction.Left, collider2D))
                can = true;
            if (_canBottom && IsTriggerFrom(Direction.Bottom, collider2D))
                can = true;
            if (_canRight && IsTriggerFrom(Direction.Right, collider2D))
                can = true;

            return can;
        }

        private bool IsTriggerFrom(Direction direction, Collider2D collider2D)
        {
            Vector2 triggerPos = collider2D.transform.position;
            Vector2 selfPos = transform.position;

            return direction switch
            {
                Direction.Top => triggerPos.y > selfPos.y + _topDistance,
                Direction.Bottom => triggerPos.y < selfPos.y - _bottomDistance,
                Direction.Left => triggerPos.x < selfPos.x - _leftDistance,
                Direction.Right => triggerPos.x > selfPos.x + _rightDistance,
                _ => false
            };
        }

        private void SetActiveByLooping() => gameObject.SetActive(_repeatable);

        #endregion

        #region CALLBACKS

        private void InvokeOnTriggeredEnter(Collider2D collider2D) => OnTriggeredEnter?.Invoke(collider2D);
        private void InvokeOnTriggeredStay(Collider2D collider2D) => OnTriggeredStay?.Invoke(collider2D);
        private void InvokeOnTriggerdExit(Collider2D collider2D) => OnTriggeredExit?.Invoke(collider2D);

        #endregion
    }
}