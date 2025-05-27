using UnityEngine;
using DG.Tweening;

namespace MyTools.UI.Animation
{
    public class AnimationAchorPos : YAnimation
    {
        [Header("Achor Pos")]
        [SerializeField] private Vector3 _visibleAchorPos;
        [SerializeField] private Vector3 _hiddenAchorPos;

        private RectTransform _rectTransform;

        private void Awake() => _rectTransform = (RectTransform)transform;

        public override Sequence AnimationOut() => DOTween.Sequence().Append(_rectTransform.DOAnchorPos(_hiddenAchorPos, _timeToHide));
        public override Sequence AnimationIn() => DOTween.Sequence().Append(_rectTransform.DOAnchorPos(_visibleAchorPos, _timeToShow));
    }
}