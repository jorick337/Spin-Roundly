using UnityEngine;

namespace MyTools.Movement.TwoDimensional
{
    public class UIMovementController : MonoBehaviour
    {
        public void LeftUp() => Movement2D.Instance.MoveDirectionX(0);
        public void LeftDown() => Movement2D.Instance.MoveDirectionX(-1);

        public void RightUp() => Movement2D.Instance.MoveDirectionX(0);
        public void RightDown() => Movement2D.Instance.MoveDirectionX(1);

        public void JumpUp() => Movement2D.Instance.JumpIfGrounded();
    }
}