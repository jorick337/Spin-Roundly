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
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private Color _visibleColor;
        private Color _hiddenColor;

        public override void Initialize()
        {
            if (_uiElement != null)
            {
                _visibleColor = _uiElement.color;
                _hiddenColor = _uiElement.color;
            }
            else
            {
                _visibleColor = _spriteRenderer.color;
                _hiddenColor = _spriteRenderer.color;
            }
            _visibleColor.a = _visibleTransparency;
            _hiddenColor.a = _hiddenTransparency;
        }

        public override Sequence AnimationIn()
        {
            if (_uiElement != null)
                return DOTween.Sequence().Append(_uiElement.DOColor(_visibleColor, _timeToShow));
            else
                return DOTween.Sequence().Append(_spriteRenderer.DOColor(_visibleColor, _timeToShow));
        }

        public override Sequence AnimationOut()
        {
            if (_uiElement != null)
                return DOTween.Sequence().Append(_uiElement.DOColor(_hiddenColor, _timeToHide));
            else
                return DOTween.Sequence().Append(_spriteRenderer.DOColor(_hiddenColor, _timeToHide));
        }
    }
}