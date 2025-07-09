using MyTools.Movement.TwoDimensional;
using MyTools.UI.Colliders;
using UnityEngine;

namespace MyTools.Levels.TwoDimensional.Objects
{
    public class JumpArea : MonoBehaviour
    {
        [SerializeField] private ColliderTrigger2D _collider2DTrigger;
        [SerializeField] private Movement2D _movement2D;

        private void OnEnable() => _collider2DTrigger.OnTriggeredEnter += Apply;
        private void OnDisable() => _collider2DTrigger.OnTriggeredEnter -= Apply;

        private void Apply(Collider2D collider2D) => _movement2D.Jump();
    }
}