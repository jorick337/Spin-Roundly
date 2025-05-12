using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace MyTools.UI.Animate
{
    public class AnimateTranparencyInUI : MonoBehaviour
    {
        [Header("Core")]
        [SerializeField] private float visibleTransparency;
        [SerializeField] private float hiddenTransparency;

        [Header("UI")]
        [SerializeField] private Graphic uiElement;

        [Header("Time")]
        [SerializeField] private float timeToShow;
        [SerializeField] private float timeToHide;

        private Color _visibleColor;
        private Color _hiddenColor;

        private Sequence _animationIn;
        private Sequence _animationOut;

        private void Awake()
        {
            _visibleColor = uiElement.color;
            _visibleColor.a = visibleTransparency;
            _hiddenColor = uiElement.color;
            _hiddenColor.a = hiddenTransparency;
        }

        private void OnDisable()
        {
            _animationIn?.Kill();
            _animationOut?.Kill();
        }

        public async UniTask AnimateInAsync()
        {
            _animationIn?.Kill();
            _animationIn = DOTween.Sequence().Append(uiElement.DOColor(_visibleColor, timeToShow));
            await _animationIn.AsyncWaitForCompletion();
        }

        public async UniTask AnimateOutAsync()
        {
            _animationOut?.Kill();
            _animationOut = DOTween.Sequence().Append(uiElement.DOColor(_hiddenColor, timeToHide));
            await _animationOut.AsyncWaitForCompletion();
        }
    }
}