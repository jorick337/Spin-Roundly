using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using MyTools.UI;
using UnityEngine;
using UnityEngine.Events;

namespace MyTools.Levels.TwoDimensional.Objects.Health
{
    public class Health_HL2 : MonoBehaviour
    {
        #region EVENTS

        public event UnityAction<int> OnChanged;
        public event Func<UniTask> OnBeforeDead;
        public event UnityAction OnDead;

        #endregion

        #region CORE

        [Header("Core")]
        [SerializeField] private Collider2DTrigger[] _collider2DTriggers;
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
                collider2DTrigger.OnTriggeredEnter += Add;
        } 

        private void OnDisable() 
        {
            foreach (var collider2DTrigger in _collider2DTriggers)
                collider2DTrigger.OnTriggeredEnter -= Add;
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

        private async void Add(Collider2D collider2D)
        {
            Current -= 1;
            InvokeOnChanged();

            if (Current == 0)
            {
                await InvokeOnBeforeDead();
                InvokeOnDead();
            }
        }

        #endregion

        #region CALLBACKS

        private async UniTask InvokeOnBeforeDead() 
        {
            foreach (Func<UniTask> handler in OnBeforeDead.GetInvocationList().Cast<Func<UniTask>>())
                await handler();
        }
        
        private void InvokeOnChanged() => OnChanged?.Invoke(Current);
        private void InvokeOnDead() => OnDead?.Invoke();

        #endregion
    }
}