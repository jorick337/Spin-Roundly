using MyTools.UI.Animate;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MyTools.Start
{
    public class StartButton : MonoBehaviour
    {
        public UnityEvent ButtonClicked;

        [SerializeField] private Button _button;
        [SerializeField] private AnimateScaleInUI _animateScaleInButton;
        [SerializeField] private AnimateScaleXInUI _animateClickButton;

        private void OnEnable() => _button.onClick.AddListener(OnClick);
        private void OnDisable() => _button.onClick.RemoveListener(OnClick);

        private void OnClick()
        {
            ButtonClicked?.Invoke();

        }
    }
}