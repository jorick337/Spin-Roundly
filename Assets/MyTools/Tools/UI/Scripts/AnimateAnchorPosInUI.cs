using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;

namespace MyTools.UI.Animate
{
    public class AnimateAnchorPosInUI : MonoBehaviour
    {
        [Header("Core")]
        [SerializeField] private Vector3 _visibleAchorPos;
        [SerializeField] private Vector3 _hiddenAchorPos;

        [Header("Time")]
        [SerializeField] private float _timeToShow;
        [SerializeField] private float _timeToHide;

        private RectTransform _rectTransform;

        private Sequence _animationIn;
        private Sequence _animationOut;

        private void Awake()
        {
            _rectTransform = (RectTransform)transform;
        }

        private void OnDisable()
        {
            _animationIn?.Kill();
            _animationOut?.Kill();
        }

        public async UniTask AnimateInAsync()
        {
            _animationIn?.Kill();
            _animationIn = DOTween.Sequence().Append(_rectTransform.DOAnchorPos(_visibleAchorPos, _timeToShow));
            await _animationIn.AsyncWaitForCompletion();
        }

        public async UniTask AnimateOutAsync()
        {
            _animationOut?.Kill();
            _animationOut = DOTween.Sequence().Append(_rectTransform.DOAnchorPos(_hiddenAchorPos, _timeToHide));
            await _animationOut.AsyncWaitForCompletion();
        }
    }
}