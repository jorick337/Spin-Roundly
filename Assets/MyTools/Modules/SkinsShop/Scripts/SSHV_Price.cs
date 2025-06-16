using UnityEngine;
using UnityEngine.UI;

namespace MyTools.Shop.Skins
{
    public class SSHV_Price : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Button _button;
        [SerializeField] private Text _text;

        // Managers
        private SSHV_Manager _shopManager;

        private void Awake() => _shopManager = SSHV_Manager.Instance;

        private void OnEnable()
        {
            _shopManager.PriceChanged += UpdatePrice;
            _shopManager.SkinChanged += UpdateActiveCanvasGroup;
            _button.onClick.AddListener(Buy);
        }

        private void OnDisable()
        {
            _shopManager.PriceChanged -= UpdatePrice;
            _shopManager.SkinChanged -= UpdateActiveCanvasGroup;
            _button.onClick.RemoveListener(Buy);
        }

        private void UpdateActiveCanvasGroup(bool active)
        {
            active = !active;
            _canvasGroup.alpha = active ? 1f : 0f;
            _canvasGroup.blocksRaycasts = active;
            _canvasGroup.interactable = active;
        }
        
        private void UpdatePrice(int cost) => UpdateText(cost.ToString());
        private void UpdateText(string text) => _text.text = text;

        private void Buy() => _shopManager.BuySkin();
    }
}