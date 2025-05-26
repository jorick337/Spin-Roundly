using System;
using Cysharp.Threading.Tasks;
using MyTools.UI.Animate;
using UnityEngine;
using UnityEngine.UI;

namespace MyTools.UI
{
    public class MyButton : MonoBehaviour
    {
        #region EVENTS

        public Func<AnimateScaleXInUI, UniTask> OnPressed;
        public Func<UniTask> OnPressEnded;

        #endregion

        #region CORE

        [Header("Core")]
        [SerializeField] protected Toggle _toggle;
        [SerializeField] protected Button _button;

        [Header("Animations")]
        [SerializeField] protected AnimateScaleInUI _animateScaleIn;
        [SerializeField] protected AnimateScaleXInUI _animateScaleXIn;

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

            if (_animateScaleIn == null)
                _animateScaleIn = GetComponent<AnimateScaleInUI>();

            if (_animateScaleXIn == null)
                _animateScaleXIn = GetComponent<AnimateScaleXInUI>();
        }

        #endregion

        #region ANIMATIONS

        public async UniTask AnimateScaleIn()
        {
            if (_animateScaleIn != null)
                await _animateScaleIn.AnimateInAsync();
        }

        public async UniTask AnimateScaleOut()
        {
            if (_animateScaleIn)
                await _animateScaleIn.AnimateOutAsync();
        }

        protected void AnimateScaleXIn() => _animateScaleXIn.Animate();

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
                await OnPressed.Invoke(_animateScaleXIn);
        }

        protected virtual async UniTask InvokeOnPressEnded()
        {
            if (OnPressEnded != null)
                await OnPressEnded.Invoke();
        }

        #endregion
    }
}