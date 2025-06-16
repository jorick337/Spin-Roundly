using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MyTools.UI.Objects
{
    public class ButtonsSelector : MonoBehaviour
    {
        private UnityAction[] _events;

        [SerializeField] private Button[] _buttons;
        [SerializeField] private Sprite _defaultSprite;
        [SerializeField] private Sprite _selectedSprite;

        private Button _currentButton;

        private void OnEnable()
        {
            _events = new UnityAction[_buttons.Length];
            for (int i = 0; i < _buttons.Length; i++)
            {
                int index = i;
                _events[i] = () => OnButtonClick(_buttons[index]);
                _buttons[i].onClick.AddListener(_events[i]);
            }
        }

        private void OnDisable()
        {
            for (int i = 0; i < _buttons.Length; i++)
                _buttons[i].onClick.RemoveListener(_events[i]);
        }

        public void Select(int index) => OnButtonClick(_buttons[index]);

        private void OnButtonClick(Button button)
        {
            if (_currentButton != null)
                _currentButton.image.sprite = _defaultSprite;

            button.image.sprite = _selectedSprite;
            _currentButton = button;
        }
    }
}