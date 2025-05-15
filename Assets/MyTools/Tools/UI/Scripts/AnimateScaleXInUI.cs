using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace MyTools.UI.Animate
{
    public class AnimateScaleXInUI : MonoBehaviour
    {
        [SerializeField] private float _visibleScaleX;
        [SerializeField] private float _hiddenScaleX;
        [SerializeField] private float _time;

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
                .Append(transform.DOScaleX(_hiddenScaleX, _time))
                .Append(transform.DOScaleX(_visibleScaleX, _time));
        }
    }
}