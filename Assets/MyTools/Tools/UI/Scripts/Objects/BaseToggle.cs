using UnityEngine;
using UnityEngine.UI;

namespace MyTools.UI.Objects.Toggles
{
    public class BaseToggle : MonoBehaviour
    {
        [Header("Core")]
        [SerializeField] protected Toggle _toggle;

        protected void OnEnable() => _toggle.onValueChanged.AddListener(OnTogglePressed);
        protected void OnDisable() => _toggle.onValueChanged.RemoveListener(OnTogglePressed);

        protected virtual void OnValidate() 
        {
            if (_toggle == null)
                _toggle = GetComponent<Toggle>();
        }

        protected virtual void OnTogglePressed(bool isOn) { }
    }
}