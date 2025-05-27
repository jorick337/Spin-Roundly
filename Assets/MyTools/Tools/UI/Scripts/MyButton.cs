using System;
using Cysharp.Threading.Tasks;
using MyTools.UI.Animation;
using UnityEngine;
using UnityEngine.UI;

namespace MyTools.UI
{
    public class MyButton : MonoBehaviour
    {
        #region EVENTS

        public Func<AnimationScaleX, UniTask> OnPressed;
        public Func<UniTask> OnPressEnded;

        #endregion

        #region CORE

        [Header("Core")]
        [SerializeField] protected Toggle _toggle;
        [SerializeField] protected Button _button;

        [Header("Animations")]
        [SerializeField] protected AnimationScale _animationScale;
        [SerializeField] protected AnimationScaleX _animationScaleX;

        #endregion

        #region MONO

        protected void OnEnable()
        {
            _button?.onClick.AddListener(ClickButtonAsync);
            _toggle?.onValueChanged.AddListener(ClickToggle);
        }

        protected void OnDisable()
        {
            _button?.onClick.RemoveListener(ClickButtonAsync);
            _toggle?.onValueChanged.RemoveListener(ClickToggle);
        }

        public virtual void OnValidate() => Validate();

        #endregion

        #region CORE LOGIC

        protected void Validate()
        {
            if (_toggle == null)
                _toggle = GetComponent<Toggle>();

            if (_button == null)
                _button = GetComponent<Button>();

            if (_animationScale == null)
                _animationScale = GetComponent<AnimationScale>();

            if (_animationScaleX == null)
                _animationScaleX = GetComponent<AnimationScaleX>();
        }

        #endregion

        #region ANIMATIONS

        public async UniTask AnimateScaleIn()
        {
            if (_animationScale != null)
                await _animationScale.AnimateInAsync();
        }

        public async UniTask AnimateScaleOut()
        {
            if (_animationScale)
                await _animationScale.AnimateOutAsync();
        }

        protected void AnimateClick() => _animationScaleX.Animate();

        #endregion

        #region CALLBACKS

        public virtual async void ClickButtonAsync()
        {
            await InvokeOnPressed();
            await InvokeOnPressEnded();
        }

        public virtual async void ClickToggle(bool isOn)
        {
            await InvokeOnPressed();
            await InvokeOnPressEnded();
        }

        protected virtual async UniTask InvokeOnPressed()
        {
            if (OnPressed != null)
                await OnPressed.Invoke(_animationScaleX);
        }

        protected virtual async UniTask InvokeOnPressEnded()
        {
            if (OnPressEnded != null)
                await OnPressEnded.Invoke();
        }

        #endregion
    }
}