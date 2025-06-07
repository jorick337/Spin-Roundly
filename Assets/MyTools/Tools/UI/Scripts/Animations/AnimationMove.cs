using DG.Tweening;
using UnityEngine;

namespace MyTools.UI.Animation
{
    public class AnimationMove : YAnimation
    {
        [Header("Move")]
        [SerializeField] private Vector3 _visibleAchorPos;
        [SerializeField] private Vector3 _hiddenAchorPos;

        private Transform _transform;

        private void Awake() => _transform = transform;

        public override Sequence AnimationOut() => DOTween.Sequence().Append(_transform.DOMove(_hiddenAchorPos, _timeToHide).SetEase(Ease.InOutSine));
        public override Sequence AnimationIn() => DOTween.Sequence().Append(_transform.DOMove(_visibleAchorPos, _timeToShow).SetEase(Ease.InOutSine));
    }
}