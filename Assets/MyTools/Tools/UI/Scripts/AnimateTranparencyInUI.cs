using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace MyTools.UI.Animate
{
    public class AnimateTranparencyInUI : MonoBehaviour
    {
        [SerializeField] private float _visibleTransparency;
        [SerializeField] private float _hiddenTransparency;
        [SerializeField] private Graphic _uiElement;
        [SerializeField] private float _timeToShow;
        [SerializeField] private float _timeToHide;

        private Color _visibleColor;
        private Color _hiddenColor;

        private Sequence _animationIn;
        private Sequence _animationOut;

        private void Awake()
        {
            _visibleColor = _uiElement.color;
            _visibleColor.a = _visibleTransparency;
            _hiddenColor = _uiElement.color;
            _hiddenColor.a = _hiddenTransparency;
        }

        private void OnDisable()
        {
            _animationIn?.Kill();
            _animationOut?.Kill();
        }

        public async UniTask AnimateInAsync()
        {
            _animationIn?.Kill();
            _animationIn = DOTween.Sequence().Append(_uiElement.DOColor(_visibleColor, _timeToShow));
            await _animationIn.AsyncWaitForCompletion();
        }

        public async UniTask AnimateOutAsync()
        {
            _animationOut?.Kill();
            _animationOut = DOTween.Sequence().Append(_uiElement.DOColor(_hiddenColor, _timeToHide));
            await _animationOut.AsyncWaitForCompletion();
        }
    }
}