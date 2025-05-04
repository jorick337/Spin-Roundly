using UnityEngine;

namespace Game.Movement.Animation
{
    public class AnimationMovement : MonoBehaviour
    {
        [Header("Core")]
        [SerializeField] private GameObjectMoving gameObjectMoving;

        [Header("UI")]
        [SerializeField] private Animator animator;

        private void Update()
        {
            UpdateJumpState();
            UpdateWalkState();
        }

        private void UpdateWalkState() => animator.SetBool("IsMoving", gameObjectMoving.IsMoving && !gameObjectMoving.IsJumping);
        private void UpdateJumpState() => animator.SetBool("IsJumping", gameObjectMoving.IsJumping);
    }
}