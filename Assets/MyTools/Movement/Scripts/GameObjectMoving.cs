using System;
using UnityEngine;

namespace Game.Movement
{
    public class GameObjectMoving : MonoBehaviour
    {
        #region EVENTS

        private Action<bool> _moveForwardHandler;
        private Action<bool> _moveBackwardHandler;
        private Action<bool> _moveLeftHandler;
        private Action<bool> _moveRightHandler;

        #endregion

        #region  CORE

        [Header("Core")]
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float jumpHeight = 0.1f;
        [SerializeField] private float gravity = -5f;

        public bool IsMoving { get; private set; } = false;
        public bool IsJumping { get; private set; } = false;

        private Vector3 _velocity;
        private Vector3 _moveInput;

        [Header("UI")]
        [SerializeField] private CharacterController characterController;
        [SerializeField] private Transform cameraTransform;

        private GameInputEvents _gameInputEvents;

        #endregion

        #region MONO

        private void Awake()
        {
            _gameInputEvents = GameInputEvents.Instance;
        }

        private void OnEnable()
        {
            _moveForwardHandler = active => UpdateInput(active, Vector3.forward);
            _moveBackwardHandler = active => UpdateInput(active, -Vector3.forward);
            _moveLeftHandler = active => UpdateInput(active, -Vector3.right);
            _moveRightHandler = active => UpdateInput(active, Vector3.right);

            _gameInputEvents.MoveForward += _moveForwardHandler;
            _gameInputEvents.MoveBackward += _moveBackwardHandler;
            _gameInputEvents.MoveLeft += _moveLeftHandler;
            _gameInputEvents.MoveRight += _moveRightHandler;
            _gameInputEvents.JumpUp += HandleJump;
        }

        private void OnDisable()
        {
            _gameInputEvents.MoveForward -= _moveForwardHandler;
            _gameInputEvents.MoveBackward -= _moveBackwardHandler;
            _gameInputEvents.MoveLeft -= _moveLeftHandler;
            _gameInputEvents.MoveRight -= _moveRightHandler;
            _gameInputEvents.JumpUp -= HandleJump;

            _moveInput = Vector3.zero;
            _velocity = Vector3.zero;

            IsMoving = false;
            IsJumping = false;
        }

        private void Update()
        {
            UpdateRotation();
            UpdateJump();
            UpdateStates();
            Move();
        }

        #endregion

        #region MOVEMENT

        private void Move()
        {
            Vector3 camForward = cameraTransform.forward;
            Vector3 camRight = cameraTransform.right;
            camForward.y = camRight.y = 0;

            Vector3 moveDir = (camForward.normalized * _moveInput.z + camRight.normalized * _moveInput.x).normalized;
            Vector3 move = moveDir * moveSpeed + _velocity;

            characterController.Move(move * Time.deltaTime);
        }

        #endregion

        #region JUMP

        private void JumpUp()
        {
            if (!IsJumping)
            {
                _velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }

        private void UpdateJump()
        {
            if (!IsJumping && _velocity.y < 0)
            {
                _velocity.y = -2f;
            }

            _velocity.y += gravity * Time.deltaTime;
        }

        #endregion

        #region UI

        private void SetActiveMovement(bool active)
        {
            characterController.enabled = active;
            enabled = active;
            if (active)
            {
                _gameInputEvents.EnableInputActions();
            }
            else
            {
                _gameInputEvents.DisableInputActions();
            }
        }

        private void UpdateRotation()
        {
            Vector3 euler = cameraTransform.eulerAngles;
            transform.rotation = Quaternion.Euler(0f, euler.y, 0f);
        }

        private void UpdateInput(bool active, Vector3 direction) => _moveInput += active ? direction : -direction;

        private void EnableAllControl() => SetActiveMovement(true);
        private void DisableAllControl() => SetActiveMovement(false);

        #endregion

        #region VALUES

        private void UpdateStates()
        {
            IsMoving = characterController.velocity.magnitude > 0.01f;
            IsJumping = !characterController.isGrounded;
        }

        #endregion

        #region CALLBACKS

        public void DoActionWithoutMovement(Action action, float timeToFrize)
        {
            DisableAllControl();
            action?.Invoke();
            Invoke(nameof(EnableAllControl), timeToFrize);
        }

        private void HandleJump() => JumpUp();

        #endregion
    }
}