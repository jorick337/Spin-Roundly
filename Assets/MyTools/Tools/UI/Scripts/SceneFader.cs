using Cysharp.Threading.Tasks;
using UnityEngine;

namespace MyTools.UI
{
    public class SceneFader : MonoBehaviour
    {
        public static SceneFader Instance;

        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _fadeDuration = 0.5f;

        private void Awake() => Instance = this;

        public async UniTask FadeIn()
        {
            _canvasGroup.blocksRaycasts = true;
            await Fade(1);
        }

        public async UniTask FadeOut()
        {
            await Fade(0);
            _canvasGroup.blocksRaycasts = false;
        }

        private async UniTask Fade(float targetAlpha)
        {
            float startAlpha = _canvasGroup.alpha;
            float time = 0;

            while (time < _fadeDuration)
            {
                time += Time.deltaTime;
                _canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / _fadeDuration);
                await UniTask.Yield();
            }

            _canvasGroup.alpha = targetAlpha;
        }
    }
}