using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace MyTools.Shop.Skins
{
    public class SSHV_Icon : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Image _image;
        [SerializeField] private bool _canChangeAlways = false;

        public UniTask Initialized => _initializedTcs.Task;

        private UniTaskCompletionSource<bool> _initializedTcs = new();

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

        private async void Initialize()
        {
            _shopManager = SSHV_Manager.Instance;
            await _shopManager.WaitUntilLoaded();
            await _shopManager.LoadBoughtSkin();
            UpdateSprite(_shopManager.Skin.Sprite);
            _initializedTcs.TrySetResult(true);
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