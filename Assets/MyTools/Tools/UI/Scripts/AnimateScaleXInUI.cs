using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Game.UITools.Animate
{
    public class AnimateScaleXInUI : MonoBehaviour
    {
        [Header("Core")]
        [SerializeField] private float visibleScaleX;
        [SerializeField] private float hiddenScaleX;

        [Header("Time")]
        [SerializeField] private float time;

        private Sequence _animate;

        private void OnDisable() => _animate?.Kill();

        public async Task AnimateAsync()
        {
            Animate();
            await _animate?.AsyncWaitForCompletion();
        }

        public void Animate()
        {
            _animate?.Kill();
            _animate = DOTween.Sequence()
                .Append(transform.DOScaleX(hiddenScaleX, time))
                .Append(transform.DOScaleX(visibleScaleX, time));
        }
    }
}