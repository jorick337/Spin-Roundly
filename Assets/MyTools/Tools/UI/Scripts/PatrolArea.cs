using UnityEngine;

namespace MyTools.Enemy
{
    public class PatrolArea : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private Transform[] _points;
        [SerializeField] private float _speed;
        [SerializeField] private float _rotationSpeed = 360f;

        private int _currentIndex = 0;

        private void Update()
        {
            Vector2 target = _points[_currentIndex].position;
            Vector2 direction = (target - _rigidbody2D.position).normalized;
            
            Vector2 newPos = Vector2.MoveTowards(_rigidbody2D.position, target, _speed * Time.fixedDeltaTime);
            _rigidbody2D?.MovePosition(newPos);

            float angularDirection = Mathf.Sign(direction.x);
            float rotationAmount = -angularDirection * _rotationSpeed * Time.fixedDeltaTime;
            _rigidbody2D?.MoveRotation(_rigidbody2D.rotation + rotationAmount);

            if (Vector2.Distance(newPos, target) < 0.1f)
                _currentIndex = (_currentIndex + 1) % _points.Length;
        }

        public void Enable() => enabled = true;
        public void Disable() => enabled = false;
    }
}