using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

namespace Game.UITools.Animate
{
    public class AnimateAnchorPosInUI : MonoBehaviour
    {
        [Header("Core")]
        [SerializeField] private Vector3 visibleAchorPos;
        [SerializeField] private Vector3 hiddenAchorPos;

        [Header("Time")]
        [SerializeField] private float timeToShow;
        [SerializeField] private float timeToHide;

        [Header("UI")]
        [SerializeField] private RectTransform rectTransform;

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
            _animationIn = DOTween.Sequence().Append(rectTransform.DOAnchorPos(visibleAchorPos, timeToShow));
            await _animationIn.AsyncWaitForCompletion();
        }

        public async Task AnimateOutAsync()
        {
            _animationOut?.Kill();
            _animationOut = DOTween.Sequence().Append(rectTransform.DOAnchorPos(hiddenAchorPos, timeToHide));
            await _animationOut.AsyncWaitForCompletion();
        }
    }
}