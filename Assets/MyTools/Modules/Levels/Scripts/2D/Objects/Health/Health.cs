using MyTools.UI;
using UnityEngine;
using UnityEngine.Events;

namespace MyTools.Levels.TwoDimensional.Objects.Health
{
    public class Health : MonoBehaviour
    {
        #region EVENTS

        public event UnityAction<int> Changed;
        public event UnityAction Dead;

        #endregion

        #region CORE

        [SerializeField] private Collider2DTrigger _collider2DTrigger;
        [SerializeField] private int _default = 3;
        [SerializeField] private bool _instance;

        public static Health Instance { get; private set; }
        public int Current { get; private set; }

        #endregion

        #region MONO

        private void Awake() => Initialize();
        private void OnEnable() => _collider2DTrigger.OnTriggered += Add;
        private void OnDisable() => _collider2DTrigger.OnTriggered -= Add; 

        #endregion

        #region INITIALIZATION

        private void Initialize()
        {
            if (_instance)
                Instance = this;

            Current = _default;
        }

        public void Restart()
        {
            Current = _default;
            InvokeChanged();
        }

        #endregion

        #region VALUES

        private void Add(Collider2D collider2D)
        {
            Current -= 1;
            InvokeChanged();

            if (Current == 0)
                InvokeDead();
        }

        #endregion

        #region CALLBACKS

        private void InvokeChanged() => Changed?.Invoke(Current);
        private void InvokeDead() => Dead?.Invoke();

        #endregion
    }
}