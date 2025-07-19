using MyTools.UI.Colliders;
using UnityEngine;
using UnityEngine.Events;

namespace MyTools.Levels.TwoDimensional.Objects.Health
{
    public class Health_HL2 : MonoBehaviour
    {
        #region EVENTS

        public event UnityAction<int> OnChanged;
        public event UnityAction OnDead;

        #endregion

        #region CORE

        [Header("Core")]
        [SerializeField] private ColliderTrigger2D[] _collider2DTriggers;
        [SerializeField] private int _default = 3;
        [SerializeField] private bool _instance;

        public static Health_HL2 Instance { get; private set; }
        public int Current { get; private set; }

        #endregion

        #region MONO

        private void Awake() => Initialize();

        private void OnEnable()
        {
            foreach (var collider2DTrigger in _collider2DTriggers)
            {
                collider2DTrigger.OnTriggeredEnter += Add;
                collider2DTrigger.OnTriggeredStay += Add;
            }
        }

        private void OnDisable()
        {
            foreach (var collider2DTrigger in _collider2DTriggers)
            {
                collider2DTrigger.OnTriggeredEnter -= Add;
                collider2DTrigger.OnTriggeredStay -= Add;
            }
        }

        #endregion

        #region INITIALIZATION

        public void Restart()
        {
            Current = _default;
            InvokeOnChanged();
        }

        private void Initialize()
        {
            if (_instance)
                Instance = this;

            Current = _default;
        }

        #endregion

        #region VALUES

        private void Add(Collider2D collider2D)
        {
            if (Current <= 0)
                return;
            
            Current -= 1;

            if (Current == 0)
                InvokeOnDead();
            else
                InvokeOnChanged();
        }

        #endregion

        #region CALLBACKS

        private void InvokeOnChanged() => OnChanged?.Invoke(Current);
        private void InvokeOnDead() => OnDead?.Invoke();

        #endregion
    }
}