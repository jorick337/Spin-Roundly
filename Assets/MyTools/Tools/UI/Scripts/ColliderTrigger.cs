using UnityEngine;
using UnityEngine.Events;

namespace MyTools.UI
{
    public class ColliderTrigger : MonoBehaviour
    {
        public UnityAction OnTriggered;

        [SerializeField] private bool _looping = false;

        private void OnTriggerEnter2D(Collider2D collider2D)
        {
            InvokeOnTriggered();
            gameObject.SetActive(_looping);
        }

        public void Enable() => gameObject.SetActive(true);

        private void InvokeOnTriggered() => OnTriggered?.Invoke();
    }
}