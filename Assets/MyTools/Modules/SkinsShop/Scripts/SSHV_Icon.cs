using UnityEngine;
using UnityEngine.UI;

namespace MyTools.Shop.Skins
{
    public class SSHV_Icon : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private bool _canChangeAlways = false;

        // Manager
        private SSHV_Manager _shopManager;

        private void Awake() => Initialize();

        private void OnEnable()
        {
            if (_canChangeAlways)
                _shopManager.OnSpriteChanged += UpdateSprite;
            _shopManager.OnSpritePurchased += UpdateSprite;
        }

        private void OnDisable()
        {
            if (_canChangeAlways)
                _shopManager.OnSpriteChanged -= UpdateSprite;
            _shopManager.OnSpritePurchased -= UpdateSprite;
        }

        private void Initialize()
        {
            _shopManager = SSHV_Manager.Instance;
            UpdateSprite(_shopManager.Skin?.Sprite);
        }

        private void UpdateSprite(Sprite sprite) => _image.sprite = sprite;
    }
}