using UnityEngine;

namespace MyTools.UI
{
    public class MapItem : MonoBehaviour
    {
        [Header("Core")]
        [SerializeField] protected ColliderTrigger _colliderTrigger;
        [SerializeField] protected ParticleSystem _particleSystem;

        protected void EnableCollider() => _colliderTrigger.Enable();
    }
}