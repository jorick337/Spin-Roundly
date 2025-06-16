using Cysharp.Threading.Tasks;
using MyTools.UI;
using MyTools.UI.Objects;
using UnityEngine;
using UnityEngine.UI;

namespace MyTools.Shop.Skins
{
    public class SkinsShopView_SSHV : MonoBehaviour
    {
        [SerializeField] private MyButton _closeButton;
        [SerializeField] private Image _iconImage;
        [SerializeField] private ButtonsSelector _buttonsSelectorSkins;

        // Managers
        private SSHV_Manager _shopManager;
        private SSHV_Provider _provider;

        private void Awake() 
        {
            _shopManager = SSHV_Manager.Instance;
            Initialize();
        }

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

        private void Initialize()
        {
            int number = _shopManager.Number;
            _shopManager.ChangeNumber(number);
            _buttonsSelectorSkins.Select(number - 1);
        }

        public void SetProvider(SSHV_Provider provider) => _provider = provider;

        private async UniTask Unload() => await _provider.UnloadAsync();
        private void UpdateIcon(Sprite sprite) => _iconImage.sprite = sprite;
    }
}