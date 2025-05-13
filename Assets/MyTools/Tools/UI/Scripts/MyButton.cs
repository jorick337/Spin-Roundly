using Cysharp.Threading.Tasks;
using MyTools.UI.Animate;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.ResourceManagement.ResourceProviders.Simulation;
using UnityEngine.UI;

namespace MyTools.UI
{
    public class MyButton : MonoBehaviour
    {
        public event UnityAction<AnimateScaleXInUI> OnPressed;
        public event UnityAction OnPressEnded;

        [SerializeField] private Button _button;
        [SerializeField] private AnimateScaleInUI _animateScaleIn;
        [SerializeField] private AnimateScaleXInUI _animateScaleXIn;

        private void OnEnable() => _button.onClick.AddListener(Click);
        private void OnDisable() => _button.onClick.RemoveListener(Click);

        private void OnValidate()
        {
            if (_button == null)
                _button = GetComponent<Button>();

            if (_animateScaleIn == null)
                _animateScaleIn = GetComponent<AnimateScaleInUI>();

            if (_animateScaleXIn == null)
                _animateScaleXIn = GetComponent<AnimateScaleXInUI>();
        }

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

        public virtual void Click()
        {
            InvokeOnPressed();
            InvokeOnPressEnded();
        }

        protected virtual void InvokeOnPressed() => OnPressed?.Invoke(_animateScaleXIn);
        protected virtual void InvokeOnPressEnded() => OnPressEnded?.Invoke();
    }
}