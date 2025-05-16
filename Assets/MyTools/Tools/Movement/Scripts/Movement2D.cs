using UnityEngine;

namespace MyTools.Movement
{
    public class Movement2D : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] SpriteRenderer _spriteRenderer;

        [SerializeField] private float _jumpForce;
        [SerializeField] private float _groundCheckRadius;
        [SerializeField] private LayerMask _groundMask;

        private Vector2 _input;
        private bool _isGrounded = false;

        private void FixedUpdate() => Move();

        private void Move()
        {
            _input.x = Input.GetAxisRaw("Horizontal");
            _rigidbody2D.linearVelocity = new Vector2(_input.x * _moveSpeed, _rigidbody2D.linearVelocity.y);

            CheckFlip();
            CheckIsGrounded();
            CheckJump();
        }

        private void CheckFlip()
        {
            if (_input.x != 0)
                _spriteRenderer.flipX = _input.x > 0;
        }

        private void CheckIsGrounded()
        {
            float extraHeight = 0.1f;
            RaycastHit2D hit = Physics2D.CircleCast(_rigidbody2D.position, _groundCheckRadius, Vector2.down, extraHeight, _groundMask);
            _isGrounded = hit.collider != null;
        }

        private void CheckJump()
        {
            if (Input.GetButtonDown("Jump") && _isGrounded)
                _rigidbody2D.linearVelocity = new Vector2(_rigidbody2D.linearVelocity.x, _jumpForce);
        }
    }
}