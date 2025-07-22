using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace MyTools.Movement.TwoDimensional
{
    [DefaultExecutionOrder(-100)]
    public class Movement2D : MonoBehaviour
    {
        public event UnityAction OnJump;

        #region CORE

        public static Movement2D Instance { get; private set; }

        [Header("Core")]
        [SerializeField] private float _moveSpeed;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] SpriteRenderer _spriteRenderer;

        [Header("Jump")]
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _groundCheckRadius;
        [SerializeField] private LayerMask _groundMask;

        private InputActions _inputActions;

        private Vector2 _keyboardInput = new();
        private Vector2 _uiInput = new();
        private Vector2 _finalInput = new();
        private bool _isGrounded = false;

        #endregion

        #region MONO

        private void Awake()
        {
            Instance = this;
            _inputActions = new();
        }

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

        private void Update()
        {
            _keyboardInput = _inputActions.GamePlay.Move.ReadValue<Vector2>();
            _finalInput = _keyboardInput + _uiInput;
            _finalInput = _finalInput.normalized;
            Move();
        }

        #endregion

        #region CORE LOGIC

        public void JumpIfGrounded()
        {
            UpdateIsGrounded();
            if (_isGrounded)
                Jump();
        }

        public void Jump()
        {
            InvokeOnJump();
            _rigidbody2D.linearVelocity = new Vector2(_rigidbody2D.linearVelocity.x, _jumpForce);
        }

        private void Move()
        {
            _rigidbody2D.linearVelocity = new Vector2(_finalInput.x * _moveSpeed, _rigidbody2D.linearVelocity.y);

            if (_finalInput.x != 0)
                _spriteRenderer.flipX = _finalInput.x > 0;
        }

        private void UpdateIsGrounded()
        {
            float extraHeight = 0.1f;
            RaycastHit2D hit = Physics2D.CircleCast(_rigidbody2D.position, _groundCheckRadius, Vector2.down, extraHeight, _groundMask);
            _isGrounded = hit.collider != null;
        }

        #endregion

        #region VALUES

        public void MoveDirectionX(float value) => _uiInput.x = value;

        public void Enable() => _inputActions.Enable();
        public void Disable() => _inputActions.Disable();

        #endregion

        #region CALLBACKS

        private void OnJumpUpStarted(InputAction.CallbackContext context) => JumpIfGrounded();
        private void InvokeOnJump() => OnJump?.Invoke();

        #endregion
    }
}