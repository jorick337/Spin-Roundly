using UnityEngine;
using UnityEngine.InputSystem;

namespace MyTools.Movement.TwoDimensional
{
    public class Movement2D : MonoBehaviour
    {
        #region CORE

        [Header("Core")]
        [SerializeField] private float _moveSpeed;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] SpriteRenderer _spriteRenderer;

        [Header("Jump")]
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _groundCheckRadius;
        [SerializeField] private LayerMask _groundMask;

        private Vector2 _input;
        private bool _isGrounded = false;

        // Managers
        private InputActions _inputActions;

        #endregion

        #region MONO

        private void Awake() => _inputActions = new();

        private void OnEnable()
        {
            Enable();
            _inputActions.GamePlay.Jump.started += OnJumpUpStarted;
        }

        private void OnDisable()
        {
            Disable();
            _inputActions.GamePlay.Jump.started -= OnJumpUpStarted;
        }

        private void Update() => Move();

        #endregion

        #region CORE LOGIC

        private void Move()
        {
            _input = _inputActions.GamePlay.Move.ReadValue<Vector2>();
            _rigidbody2D.linearVelocity = new Vector2(_input.x * _moveSpeed, _rigidbody2D.linearVelocity.y);

            if (_input.x != 0)
                _spriteRenderer.flipX = _input.x > 0;
        }

        private void Jump()
        {
            UpdateIsGrounded();
            if (_isGrounded)
                _rigidbody2D.linearVelocity = new Vector2(_rigidbody2D.linearVelocity.x, _jumpForce);
        }

        private void UpdateIsGrounded()
        {
            float extraHeight = 0.1f;
            RaycastHit2D hit = Physics2D.CircleCast(_rigidbody2D.position, _groundCheckRadius, Vector2.down, extraHeight, _groundMask);
            _isGrounded = hit.collider != null;
        }

        #endregion

        #region VALUES

        public void Enable() => _inputActions.Enable();
        public void Disable() => _inputActions.Disable();

        #endregion

        #region CALLBACKS

        private void OnJumpUpStarted(InputAction.CallbackContext context) => Jump();

        #endregion
    }
}