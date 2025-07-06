using UnityEngine;
using UnityEngine.UI;

namespace MyTools.UI.Objects.Buttons
{
    public class BaseButton : MonoBehaviour
    {
        [Header("Core")]
        [SerializeField] protected Button _button;

        protected void OnEnable() => _button.onClick.AddListener(OnButtonPressed);
        protected void OnDisable() => _button.onClick.RemoveListener(OnButtonPressed);

        protected virtual void OnValidate() 
        {
            if (_button == null)
                _button = GetComponent<Button>();
        }

        protected virtual void OnButtonPressed() { }
    }
}