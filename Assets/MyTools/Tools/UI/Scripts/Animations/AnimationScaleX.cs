using DG.Tweening;
using UnityEngine;

namespace MyTools.UI.Animation
{
    public class AnimationScaleX : YAnimation
    {
        [Header("Scale")]
        [SerializeField] private float _visibleScale;
        [SerializeField] private float _hiddenScale;

        public override Sequence AnimationIn() => DOTween.Sequence().Append(transform.DOScaleX(_visibleScale, _timeToShow));
        public override Sequence AnimationOut() => DOTween.Sequence().Append(transform.DOScaleX(_hiddenScale, _timeToHide));
    }
}