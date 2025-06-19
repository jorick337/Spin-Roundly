using UnityEngine;
using UnityEngine.UI;

namespace MyTools.Shop.Skins
{
    public class SSHV_Icon : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private bool _isPurchased = false;

        // Manager
        private SSHV_Manager _shopManager;

        private void Awake() => _shopManager = SSHV_Manager.Instance;
        
        private void OnEnable()
        {
            if (_isPurchased)
                _shopManager.OnSpritePurchased += UpdateSprite;
            else
                _shopManager.OnSpriteChanged += UpdateSprite;
        }
        private void OnDisable()
        {
            if (_isPurchased)
                _shopManager.OnSpritePurchased -= UpdateSprite;
            else
                _shopManager.OnSpriteChanged -= UpdateSprite;
        }

        private void UpdateSprite(Sprite sprite) => _image.sprite = sprite;
    }
}