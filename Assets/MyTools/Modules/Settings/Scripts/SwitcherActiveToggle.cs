using Game.Localization;
using MyTools.UI.Animate;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MyTools.Settings
{
    public class SwitcherActiveToggle : MonoBehaviour
    {
        public event UnityAction OnPressed;
        public event UnityAction<bool> OnPressEnded;

        [SerializeField] private Toggle _toggle;
        [SerializeField] private ToggleTextLocalization _toggleTextLocalization;
        [SerializeField] private AnimateScaleXInUI _animateScaleXIn;

        private void OnEnable() => _toggle.onValueChanged.AddListener(Click);
        private void OnDisable() => _toggle.onValueChanged.RemoveListener(Click);

        public void UpdateActivity(bool activity) 
        {
            _toggle.isOn = activity;
            _toggleTextLocalization.SetToggleState(activity);
        }

        private void Click(bool isOn)
        {
            _animateScaleXIn.Animate();
            InvokeOnPressed();
            InvokeOnPressEnded();
            _toggleTextLocalization.SetToggleState(isOn);
        }

        private void InvokeOnPressed() => OnPressed?.Invoke();
        private void InvokeOnPressEnded() => OnPressEnded?.Invoke(_toggle.isOn);
    }
}