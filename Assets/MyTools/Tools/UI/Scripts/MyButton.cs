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

        [SerializeField] private Button _button;
        [SerializeField] private AnimateScaleInUI _animateScaleIn;
        [SerializeField] private AnimateScaleXInUI _animateScaleXIn;

        #endregion

        #region MONO

        private void OnEnable() => _button.onClick.AddListener(ClickAsync);
        private void OnDisable() => _button.onClick.RemoveListener(ClickAsync);

        private void OnValidate()
        {
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

        #endregion

        #region CALLBACKS

        public virtual async void ClickAsync()
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