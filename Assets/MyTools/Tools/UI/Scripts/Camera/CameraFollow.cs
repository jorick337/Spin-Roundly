using UnityEngine;

namespace MyTools.UI.CameraSystem
{
    public class CameraFollower : MonoBehaviour
    {
        [Header("Core")]
        [SerializeField] private float _speed;
        [SerializeField] private float _yOffSet;
        [SerializeField] private bool _isLooping;
        [SerializeField] private Transform _target;

        [Header("Limits")]
        [SerializeField] private float _minX;
        [SerializeField] private float _maxX;
        [SerializeField] private float _minY;
        [SerializeField] private float _maxY;

        private Transform _mainCamera;

        private void Awake() => _mainCamera = Camera.main.transform;

        private void LateUpdate() 
        {
            if (_isLooping)
                Follow();
        }

        private void Follow()
        {
            Vector3 newPos = new(_target.position.x, _target.position.y + _yOffSet, -10f);
            Vector3 smoothPos = Vector3.Slerp(transform.position, newPos, _speed * Time.deltaTime);

            float clampedX = Mathf.Clamp(smoothPos.x, _minX, _maxX);
            float clampedY = Mathf.Clamp(smoothPos.y, _minY, _maxY);

            _mainCamera.position = new Vector3(clampedX, clampedY, -10f);
        }

        public void Enable() => _isLooping = true;
        public void Disable() => _isLooping = false;
    }
}