using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace MyTools.UI.Animation
{
    public class AnimationTransparency : YAnimation
    {
        [Header("Transparency")]
        [SerializeField] private float _visibleTransparency;
        [SerializeField] private float _hiddenTransparency;
        [SerializeField] private Graphic _uiElement;

        private Color _visibleColor;
        private Color _hiddenColor;

        private void Awake()
        {
            _visibleColor = _uiElement.color;
            _visibleColor.a = _visibleTransparency;
            _hiddenColor = _uiElement.color;
            _hiddenColor.a = _hiddenTransparency;
        }

        public override Sequence AnimationIn() => DOTween.Sequence().Append(_uiElement.DOColor(_visibleColor, _timeToShow));
        public override Sequence AnimationOut() => DOTween.Sequence().Append(_uiElement.DOColor(_hiddenColor, _timeToHide));
    }
}