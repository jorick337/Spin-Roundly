using MyTools.Movement.TwoDimensional;
using MyTools.UI.Colliders;
using UnityEngine;

namespace MyTools.Levels.TwoDimensional.Objects
{
    public class JumpArea : TriggerActivator
    {
        private Movement2D _movement2D;

        private void Awake() => _movement2D = Movement2D.Instance;

        protected override void Enter(Collider2D collider2D) => _movement2D.Jump();
    }
}