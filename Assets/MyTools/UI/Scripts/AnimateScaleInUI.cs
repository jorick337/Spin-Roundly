using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Game.UITools.Animate
{
    public class AnimateScaleInUI : MonoBehaviour
    {
        [Header("Core")]
        [SerializeField] private Vector3 visibleScale;
        [SerializeField] private Vector3 hiddenScale;

        [Header("Time")]
        [SerializeField] private float timeToShow;
        [SerializeField] private float timeToHide;

        private Sequence _animationIn;
        private Sequence _animationOut;

        private void OnDisable()
        {
            _animationIn?.Kill();
            _animationOut?.Kill();
        }

        public async Task AnimateInAsync()
        {
            _animationIn?.Kill();
            _animationIn = DOTween.Sequence().Append(transform.DOScale(visibleScale, timeToShow));
            await _animationIn.AsyncWaitForCompletion();
        }

        public async Task AnimateOutAsync()
        {
            _animationOut?.Kill();
            _animationOut = DOTween.Sequence().Append(transform.DOScale(hiddenScale, timeToHide));
            await _animationOut.AsyncWaitForCompletion();
        }
    }
}