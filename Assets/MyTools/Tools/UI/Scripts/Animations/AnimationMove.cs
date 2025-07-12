using DG.Tweening;
using UnityEngine;

namespace MyTools.UI.Animation
{
    public class AnimationMove : YAnimation
    {
        [Header("Move")]
        [SerializeField] private Vector3 _visibleAchorPos;
        [SerializeField] private Vector3 _hiddenAchorPos;
        [SerializeField] private bool _useStartPos = false;

        private Transform _transform;
        private Vector3 _currentHiddenAchorPos;

        protected override void Awake()
        {
            base.Awake();
            _currentHiddenAchorPos = _hiddenAchorPos;
        }

        public override void Initialize()
        {
            _transform = transform;
            if (_useStartPos)
            {
                _visibleAchorPos = transform.position;
                _currentHiddenAchorPos = _visibleAchorPos + _hiddenAchorPos;
            }
        }

        public override Sequence AnimationOut() => DOTween.Sequence().Append(_transform.DOMove(_currentHiddenAchorPos, _timeToHide).SetEase(Ease.InOutSine));
        public override Sequence AnimationIn() => DOTween.Sequence().Append(_transform.DOMove(_visibleAchorPos, _timeToShow).SetEase(Ease.InOutSine));
    }
}