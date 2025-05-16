using UnityEngine;

namespace MyTools.Movement
{
    public class CursorManager : MonoBehaviour
    {
        [Header("Core")]
        [SerializeField] private bool activateCursor = true;

        private void Awake()
        {
            UpdateVisibleAndLockState(activateCursor);
        }

        public void HideAndLockCursor() => UpdateVisibleAndLockState(false);
        public void ShowAndUnlockCursor() => UpdateVisibleAndLockState(true);

        private void UpdateVisibleAndLockState(bool active)
        {
            Cursor.visible = active;
            Cursor.lockState = active ? CursorLockMode.Confined : CursorLockMode.Locked;
        }
    }
}