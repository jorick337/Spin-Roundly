using UnityEngine;

namespace MyTools.Movement.TwoDimensional
{
    public class CameraFollower : MonoBehaviour
    {
        [Header("Follow")]
        [SerializeField] private float _speed;
        [SerializeField] private float _yOffSet;
        [SerializeField] private Transform _target;

        private Transform _mainCamera;

        [Header("Limits")]
        [SerializeField] private float _minX;
        [SerializeField] private float _maxX;
        [SerializeField] private float _minY;
        [SerializeField] private float _maxY;

        private void Awake() => _mainCamera = Camera.main.transform;

        private void Update()
        {
            Vector3 newPos = new(_target.position.x, _target.position.y + _yOffSet, -10f);
            Vector3 smoothPos = Vector3.Slerp(transform.position, newPos, _speed * Time.deltaTime);

            float clampedX = Mathf.Clamp(smoothPos.x, _minX, _maxX);
            float clampedY = Mathf.Clamp(smoothPos.y, _minY, _maxY);

            _mainCamera.position = new Vector3(clampedX, clampedY, -10f);
        }
    }
}