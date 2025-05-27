using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace MyTools.UI.Animation
{
    public abstract class YAnimation : MonoBehaviour
    {
        #region CORE

        [Header("Core")]
        [SerializeField] protected float _timeToShow;
        [SerializeField] protected float _timeToHide;
        [SerializeField] private bool _isLooping = false;

        public abstract Sequence AnimationIn();
        public abstract Sequence AnimationOut();

        private Sequence _animationIn;
        private Sequence _animationOut;
        private Sequence _animation;

        #endregion

        #region MONO

        private void Start()
        {
            if (_isLooping)
                AnimateAlways();
        }

        private void OnDisable() => KillAnimations();
        private void OnDestroy() => KillAnimations();

        #endregion

        #region INITIALIZATION

        private void KillAnimations()
        {
            _animationIn?.Kill();
            _animationOut?.Kill();
        }

        private async void AnimateAlways()
        {
            var token = this.GetCancellationTokenOnDestroy();
            try
            {
                while (true)
                    await AnimateAsync().AttachExternalCancellation(token);
            }
            catch (OperationCanceledException) { }
        }

        #endregion

        #region CORE LOGIC

        public void AnimateIn()
        {
            _animationIn?.Kill();
            _animationIn = AnimationIn();
        }

        public void AnimateOut()
        {
            _animationOut?.Kill();
            _animationOut = AnimationOut();
        }

        public void Animate()
        {
            _animation?.Kill();
            _animation = DOTween.Sequence().Append(AnimationIn()).Append(AnimationOut());
        }

        public async UniTask AnimateInAsync()
        {
            AnimateIn();
            await _animationIn.AsyncWaitForCompletion();
        }

        public async UniTask AnimateOutAsync()
        {
            AnimateOut();
            await _animationOut.AsyncWaitForCompletion();
        }

        public async UniTask AnimateAsync()
        {
            await AnimateInAsync();
            await AnimateOutAsync();
        }

        #endregion
    }
}