using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace MyTools.UI.CameraSystem
{
    public class CameraShaker : MonoBehaviour
    {
        [SerializeField] private float _defaultDuration = 0.1f;
        [SerializeField] private float _defaultMagnitude = 0.2f;

        private Transform _mainCamera;
        private Vector3 _initialPos;

        private bool _isFinish;

        private void Awake() => _mainCamera = Camera.main.transform;

        public void Shake(float duration, float magnitude)
        {
            StopAllCoroutines();
            StartCoroutine(ShakeCoroutine(duration, magnitude));
        }

        public void Shake() => Shake(_defaultDuration, _defaultMagnitude);

        public async UniTask WaitUntilFinished() => await UniTask.WaitUntil(() => _isFinish == true);

        private IEnumerator ShakeCoroutine(float duration, float magnitude)
        {
            _isFinish = false;
            _initialPos = _mainCamera.position;
            float time = 0f;

            while (time < duration)
            {
                float offsetX = Random.Range(-1f, 1f) * magnitude;
                float offsetY = Random.Range(-1f, 1f) * magnitude;

                _mainCamera.position = _initialPos + new Vector3(offsetX, offsetY, 0f);

                time += Time.deltaTime;
                yield return null;
            }

            _mainCamera.position = _initialPos;
            _isFinish = true;
        }
    }
}