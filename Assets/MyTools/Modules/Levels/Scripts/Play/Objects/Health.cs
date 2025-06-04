using MyTools.UI;
using UnityEngine;
using UnityEngine.Events;

namespace MyTools.Levels.Play
{
    public class Health : MonoBehaviour
    {
        public event UnityAction<int> Changed;
        public event UnityAction Dead;

        [SerializeField] private Collider2DTrigger _collider2DTrigger;
        [SerializeField] private int _default = 3;
        [SerializeField] private bool _instance;

        public static Health Instance { get; private set; }

        public int Current { get; private set; }

        private void Awake() => Initialize();
        private void OnEnable() => _collider2DTrigger.OnTriggered += Add;
        private void OnDisable() => _collider2DTrigger.OnTriggered -= Add;

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

        private void Add(Collider2D collider2D)
        {
            Current -= 1;
            InvokeChanged();

            if (Current == 0)
                InvokeDead();
        }

        private void InvokeChanged() => Changed?.Invoke(Current);
        private void InvokeDead() => Dead?.Invoke();
    }
}
