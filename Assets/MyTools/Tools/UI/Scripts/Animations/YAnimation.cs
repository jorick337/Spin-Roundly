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
            _animation?.Kill();
            DOTween.Kill(this);
        }

        #endregion

        #region CORE LOGIC

        public void AnimateIn()
        {
            if (!IsValid()) return;

            _animationIn?.Kill();
            _animationIn = AnimationIn();
        }

        public void AnimateOut()
        {
            if (!IsValid()) return;

            _animationOut?.Kill();
            _animationOut = AnimationOut();
        }

        public void Animate()
        {
            if (!IsValid()) return;

            _animation?.Kill();
            _animation = DOTween.Sequence().Append(AnimationIn()).Append(AnimationOut());
        }

        #endregion

        #region ASYNC

        public async UniTask AnimateInAsync()
        {
            if (!IsValid()) return;

            AnimateIn();
            await _animationIn.AsyncWaitForCompletion();
        }

        public async UniTask AnimateOutAsync()
        {
            if (!IsValid()) return;

            AnimateOut();
            await _animationOut.AsyncWaitForCompletion();
        }

        public async UniTask AnimateAsync()
        {
            if (!IsValid()) return;

            await AnimateInAsync();
            await AnimateOutAsync();
        }

        #endregion

        #region ALWAYS

        public void StartAlwaysAnimation()
        {
            if (_isLooping)
                return;
                
            _isLooping = true;
            AnimateAlways();
        }
        
        public void StopAlwaysAnimation() 
        {
            _isLooping = false;
            AnimateIn();
        }

        private async void AnimateAlways()
        {
            var token = this.GetCancellationTokenOnDestroy();
            try
            {
                while (IsValid() && _isLooping)
                    await AnimateAsync().AttachExternalCancellation(token);
            }
            catch (OperationCanceledException) { }
        }

        #endregion

        #region VALUES

        private bool IsValid() => this != null && gameObject != null && gameObject.activeInHierarchy;

        #endregion
    }
}