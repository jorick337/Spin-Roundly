using Cysharp.Threading.Tasks;
using MyTools.UI.Animate;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MyTools.Start
{
    public class StartButton : MonoBehaviour
    {
        public event UnityAction<AnimateScaleXInUI> OnPressed;
        public event UnityAction OnPressEnded;

        [SerializeField] private Button _button;
        [SerializeField] private AnimateScaleInUI _animateScaleIn;
        [SerializeField] private AnimateScaleXInUI _animateScaleXIn;

        private void OnEnable() => _button.onClick.AddListener(Click);
        private void OnDisable() => _button.onClick.RemoveListener(Click);

        public async UniTask AnimateScaleIn() => await _animateScaleIn.AnimateInAsync();
        public async UniTask AnimateScaleOut() => await _animateScaleIn.AnimateOutAsync();

        private void Click()
        {
            InvokeOnPressed();
            InvokeOnPressEnded();
        }

        private void InvokeOnPressed() => OnPressed?.Invoke(_animateScaleXIn);
        private void InvokeOnPressEnded() => OnPressEnded?.Invoke();
    }
}