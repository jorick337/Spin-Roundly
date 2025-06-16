using Cysharp.Threading.Tasks;
using MyTools.UI;
using UnityEngine;
using UnityEngine.UI;

namespace MyTools.Shop.Skins
{
    public class SkinsShopView_SSHV : MonoBehaviour
    {
        [SerializeField] private MyButton _closeButton;
        [SerializeField] private Image _iconImage;

        // Managers
        private SSHV_Manager _shopManager;
        private SSHV_Provider _provider;

        private void Awake() => _shopManager = SSHV_Manager.Instance;

        private void OnEnable()
        {
            _closeButton.OnPressed += Unload;
            _shopManager.SpriteChanged += UpdateIcon;
        }

        private void OnDisable()
        {
            _closeButton.OnPressed -= Unload;
            _shopManager.SpriteChanged -= UpdateIcon;
        }

        public void SetProvider(SSHV_Provider provider) => _provider = provider;

        private async UniTask Unload() => await _provider.UnloadAsync();
        private void UpdateIcon(Sprite sprite) => _iconImage.sprite = sprite;
    }
}