using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

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

        [Header("Directions")]
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

        private void OnTriggerEnter2D(Collider2D collider2D)
        {
            if (!collider2D.transform.CompareTag(_tag))
                return;

            bool can = false;

            if (_canTop && IsTriggerFrom(Direction.Top, collider2D))
                can = true;
            if (_canLeft && IsTriggerFrom(Direction.Left, collider2D))
                can = true;
            if (_canBottom && IsTriggerFrom(Direction.Bottom, collider2D))
                can = true;
            if (_canRight && IsTriggerFrom(Direction.Right, collider2D))
                can = true;

            if (!can)
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

        private void InvokeOnTriggered(Collider2D collider2D) => OnTriggered?.Invoke(collider2D);

        #endregion
    }
}