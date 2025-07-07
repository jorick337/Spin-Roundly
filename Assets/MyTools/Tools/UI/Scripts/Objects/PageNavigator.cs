using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MyTools.UI.Objects
{
    public class PageNavigator : MonoBehaviour
    {
        public event UnityAction<int> PageChanged;
        
        [SerializeField] protected Button _leftButton;
        [SerializeField] protected Button _rightButton;

        protected int _numberPage = 0;

        private void OnEnable()
        {
            _leftButton.onClick.AddListener(PrevPage);
            _rightButton.onClick.AddListener(NextPage);
        }

        private void OnDisable()
        {
            _leftButton.onClick.RemoveListener(PrevPage);
            _rightButton.onClick.RemoveListener(NextPage);
        }

        private void Turn(int direction)
        {
            _numberPage += direction;

            SetActiveLeftButton(_numberPage != 0);
            SetActiveRightButton(_numberPage != 1);

            InvokePageChanged();
        }

        private void SetActiveRightButton(bool active) => _rightButton.gameObject.SetActive(active);
        private void SetActiveLeftButton(bool active) => _leftButton.gameObject.SetActive(active);

        private void NextPage() => Turn(1);
        private void PrevPage() => Turn(-1);

        private void InvokePageChanged() => PageChanged?.Invoke(_numberPage);
    }
}