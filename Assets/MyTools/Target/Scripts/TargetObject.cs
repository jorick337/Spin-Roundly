using UnityEngine;

namespace Game.Target
{
    public class TargetObject : MonoBehaviour
    {
        [Header("Core")]
        [SerializeField] private int index;

        [Header("Managers")]
        [SerializeField] private TargetsManager targetsManager;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                MarkAsFound();
                DestroySelf();
            }
        }

        public void Initialize(bool active)
        {
            if (active)
            {
                DestroySelf();
            }
        }

        private void MarkAsFound() => targetsManager.MarkAsFound(index);

        private void DestroySelf() => Destroy(gameObject);
    }
}