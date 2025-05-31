using UnityEngine;

namespace MyTools.Movement.TwoDimensional
{
    public class RotateMoving : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private float _rotationSpeed;

        private void Update() 
        {
            float horizontalSpeed = _rigidbody2D.linearVelocityX;
            float speedFactor = _rigidbody2D.linearVelocity.magnitude;

            float rotationAmount = -Mathf.Sign(horizontalSpeed) * speedFactor * _rotationSpeed * Time.deltaTime;
            _rigidbody2D.MoveRotation(_rigidbody2D.rotation + rotationAmount);
        }
    }
}