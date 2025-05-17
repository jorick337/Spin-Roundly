using UnityEngine;
using UnityEngine.Events;

namespace MyTools.UI
{
    public class ColliderTrigger : MonoBehaviour
    {
        public UnityAction OnTriggered;

        private void OnTriggerEnter2D(Collider2D collider2D) => InvokeOnTriggered();

        private void InvokeOnTriggered() => OnTriggered?.Invoke();
    }
}