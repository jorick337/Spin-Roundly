using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace MyTools.Movement
{
    public class GameInputEvents : MonoBehaviour
    {
        #region EVENTS

        public UnityAction<bool> MoveForward;
        public UnityAction<bool> MoveBackward;
        public UnityAction<bool> MoveLeft;
        public UnityAction<bool> MoveRight;

        public UnityAction JumpUp;

        #endregion

        #region CORE

        public static GameInputEvents Instance { get; private set; }

        public InputActions InputActions { get; private set; }

        private bool _isLoaded = false;

        #endregion

        #region MONO

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                
                InputActions = new InputActions();
                _isLoaded = true;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void OnEnable()
        {
            if (_isLoaded)
            {
                EnableInputActions();

                // InputActions.GamePlay.Forward.started += OnMoveForwardStarted; // W
                // InputActions.GamePlay.Forward.canceled += OnMoveForwardCanceled;

                // InputActions.GamePlay.Backward.started += OnMoveBackwardStarted; // S
                // InputActions.GamePlay.Backward.canceled += OnMoveBackwardCanceled;

                // InputActions.GamePlay.Right.started += OnMoveLeftStarted; // A
                // InputActions.GamePlay.Left.canceled += OnMoveLeftCanceled;

                // InputActions.GamePlay.Right.started += OnMoveRightStarted; // D
                // InputActions.GamePlay.Right.canceled += OnMoveRightCanceled;

                // InputActions.GamePlay.Jump.started += OnJumpUpStarted; // SPASE
            }
        }

        private void OnDisable()
        {
            if (_isLoaded)
            {
                DisableInputActions();

                // InputActions.GamePlay.Forward.started -= OnMoveForwardStarted; // W
                // InputActions.GamePlay.Forward.canceled -= OnMoveForwardCanceled;

                // InputActions.GamePlay.Backward.started -= OnMoveBackwardStarted; // S
                // InputActions.GamePlay.Backward.canceled -= OnMoveBackwardCanceled;

                // InputActions.GamePlay.Left.started -= OnMoveLeftStarted; // A
                // InputActions.GamePlay.Left.canceled -= OnMoveLeftCanceled;

                // InputActions.GamePlay.Right.started -= OnMoveRightStarted; // D
                // InputActions.GamePlay.Right.canceled -= OnMoveRightCanceled;

                // InputActions.GamePlay.Jump.started -= OnJumpUpStarted; // SPASE
            }
        }

        #endregion

        #region CORE LOGIC

        public void EnableInputActions() => InputActions.Enable();
        public void DisableInputActions() => InputActions.Disable();

        #endregion

        #region VALUES



        #endregion

        #region CALLBACKS

        private void OnMoveForwardStarted(InputAction.CallbackContext context) => MoveForward?.Invoke(true);
        private void OnMoveForwardCanceled(InputAction.CallbackContext context) => MoveForward?.Invoke(false);

        private void OnMoveBackwardStarted(InputAction.CallbackContext context) => MoveBackward?.Invoke(true);
        private void OnMoveBackwardCanceled(InputAction.CallbackContext context) => MoveBackward?.Invoke(false);

        private void OnMoveLeftStarted(InputAction.CallbackContext context) => MoveLeft?.Invoke(true);
        private void OnMoveLeftCanceled(InputAction.CallbackContext context) => MoveLeft?.Invoke(false);

        private void OnMoveRightStarted(InputAction.CallbackContext context) => MoveRight?.Invoke(true);
        private void OnMoveRightCanceled(InputAction.CallbackContext context) => MoveRight?.Invoke(false);

        private void OnJumpUpStarted(InputAction.CallbackContext context) => JumpUp?.Invoke();

        #endregion
    }
}