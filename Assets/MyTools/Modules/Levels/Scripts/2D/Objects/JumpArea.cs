using MyTools.Movement.TwoDimensional;
using MyTools.UI;
using UnityEngine;

namespace MyTools.Levels.TwoDimensional.Objects
{
    public class JumpArea : MonoBehaviour
    {
        [SerializeField] private Collider2DTrigger _collider2DTrigger;
        [SerializeField] private Movement2D _movement2D;

        private void OnEnable() => _collider2DTrigger.OnTriggeredEnter += Apply;
        private void OnDisable() => _collider2DTrigger.OnTriggeredEnter -= Apply;

        private void Apply(Collider2D collider2D) => _movement2D.Jump();
    }
}