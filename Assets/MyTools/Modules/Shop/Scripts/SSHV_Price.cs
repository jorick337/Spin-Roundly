using UnityEngine;
using UnityEngine.UI;

namespace MyTools.Shop.Skins
{
    public class SSHV_Price : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Text _text;

        // Managers
        private SSHV_Manager _shopManager;

        private void Awake() => _shopManager = SSHV_Manager.Instance;

        private void OnEnable()
        {
            _shopManager.PriceChanged += UpdatePrice;
            _shopManager.SkinChanged += UpdateActivity;
            _button.onClick.AddListener(Buy);
        }

        private void OnDisable()
        {
            _shopManager.PriceChanged -= UpdatePrice;
            _shopManager.SkinChanged -= UpdateActivity;
            _button.onClick.RemoveListener(Buy);
        }

        private void UpdatePrice(int cost) => UpdateText(cost.ToString());

        private void UpdateText(string text) => _text.text = text;
        private void UpdateActivity(bool active) => gameObject.SetActive(active);

        private void Buy() => _shopManager.BuySkin();
    }
}