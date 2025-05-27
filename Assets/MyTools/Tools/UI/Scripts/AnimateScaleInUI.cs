using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace MyTools.UI.Animate
{
    public class AnimateScaleInUI : MonoBehaviour
    {
        [SerializeField] private Vector3 _visibleScale;
        [SerializeField] private Vector3 _hiddenScale;
        [SerializeField] private float _timeToShow;
        [SerializeField] private float _timeToHide;
        [SerializeField] private bool _isLooping = false;

        private Sequence _animationIn;
        private Sequence _animationOut;

        private void Start()
        {
            if (_isLooping)
                AnimateAlways();
        }

        private void OnDisable() => KillAnimations();
        private void OnDestroy() => KillAnimations();

        public async UniTask AnimateInAsync()
        {
            _animationIn?.Kill();
            _animationIn = DOTween.Sequence().Append(transform.DOScale(_visibleScale, _timeToShow));
            await _animationIn.AsyncWaitForCompletion();
        }

        public async UniTask AnimateOutAsync()
        {
            _animationOut?.Kill();
            _animationOut = DOTween.Sequence().Append(transform.DOScale(_hiddenScale, _timeToHide));
            await _animationOut.AsyncWaitForCompletion();
        }

        private async void AnimateAlways()
        {
            var token = this.GetCancellationTokenOnDestroy();
            try
            {
                while (true)
                {
                    await AnimateInAsync().AttachExternalCancellation(token);
                    await AnimateOutAsync().AttachExternalCancellation(token);
                }
            }
            catch (OperationCanceledException){}
        }

        private void KillAnimations()
        {
            _animationIn?.Kill();
            _animationOut?.Kill();
        }
    }
}