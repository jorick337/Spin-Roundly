using UnityEngine;

namespace MyTools.UI
{
    public class Teleport : MonoBehaviour
    {
        [SerializeField] private Transform _targetTrans;
        [SerializeField] private Transform _objTrans;
        [SerializeField] private Rigidbody2D _rigidbody2D;

        private bool _haveRb = false;

        private void Awake() => _haveRb = _rigidbody2D != null;

        public void SendToTarget() 
        {
            if (_haveRb)
            {
                DisableSimulateRigidbody2D();
                UpdatePos();
                ResetSpeed();
                EnableSimulateRigidbody2D();
            }
            else
                UpdatePos();
        }

        private void ResetSpeed()
        {
            _rigidbody2D.linearVelocity = Vector2.zero;
            _rigidbody2D.linearVelocityY = 0f;
            _rigidbody2D.angularVelocity = 0f;
        }
        
        private void UpdatePos() => _objTrans.position = _targetTrans.position;

        private void EnableSimulateRigidbody2D() => _rigidbody2D.simulated = true;
        private void DisableSimulateRigidbody2D() => _rigidbody2D.simulated = false;
    }
}