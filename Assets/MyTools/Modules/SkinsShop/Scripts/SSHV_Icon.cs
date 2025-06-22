using UnityEngine;
using UnityEngine.UI;

namespace MyTools.Shop.Skins
{
    public class SSHV_Icon : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Image _image;
        [SerializeField] private bool _canChangeAlways = false;
        [SerializeField] private bool _canInitialize = false;

        // Manager
        private SSHV_Manager _shopManager;

        private void Awake()
        {
            _shopManager = SSHV_Manager.Instance;
            Initialize();
        }

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

        private async void Initialize() 
        {
            if (_canInitialize)
            {
                await _shopManager.WaitUntilLoaded();
                UpdateSprite(_shopManager.Skin.Sprite);
            }
        } 

        private void UpdateSprite(Sprite sprite)
        {
            if (sprite != null)
            {
                if (_image != null)
                    _image.sprite = sprite;
                else if (_spriteRenderer != null)
                    _spriteRenderer.sprite = sprite;
            }
        }
    }
}