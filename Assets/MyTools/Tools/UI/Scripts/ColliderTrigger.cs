using UnityEngine;
using UnityEngine.Events;

namespace MyTools.UI
{
    public class ColliderTrigger : MonoBehaviour
    {
        public UnityAction TriggerEnter2D;
        public UnityAction<Collision2D> CollisionEnter2D;

        [SerializeField] private bool _looping = false;

        private void OnTriggerEnter2D(Collider2D collider2D)
        {
            InvokeTriggerEnter2D();
            gameObject.SetActive(_looping);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            InvokeCollisionEnter2D(collision);
            gameObject.SetActive(_looping);
        }

        public void Enable() => gameObject.SetActive(true);
        public void Disable() => gameObject.SetActive(false);

        private void InvokeTriggerEnter2D() => TriggerEnter2D?.Invoke();
        private void InvokeCollisionEnter2D(Collision2D collision2D) => CollisionEnter2D?.Invoke(collision2D);
    }
}