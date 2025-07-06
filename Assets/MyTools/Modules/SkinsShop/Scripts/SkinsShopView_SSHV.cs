using Cysharp.Threading.Tasks;
using MyTools.UI;
using MyTools.UI.Objects;
using UnityEngine;

namespace MyTools.Shop.Skins
{
    public class SkinsShopView_SSHV : MonoBehaviour
    {
        [SerializeField] private MyButton _closeButton;
        [SerializeField] private ButtonsSelector _buttonsSelectorSkins;

        // Managers
        private SSHV_Manager _shopManager;
        private ShopProvider _provider;

        private void Awake() 
        {
            _shopManager = SSHV_Manager.Instance;
            Initialize();
        }

        private void OnEnable() => _closeButton.OnPressed += Unload;
        private void OnDisable() => _closeButton.OnPressed -= Unload;

        private void Initialize()
        {
            int number = _shopManager.Number;
            _shopManager.ChangeNumber(number);
            _buttonsSelectorSkins.Select(number - 1);
        }

        public void SetProvider(ShopProvider provider) => _provider = provider;

        private async UniTask Unload() => await _provider.UnloadAsync();
    }
}