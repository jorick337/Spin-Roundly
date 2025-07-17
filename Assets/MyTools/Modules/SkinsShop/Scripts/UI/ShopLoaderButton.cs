using MyTools.Shop.Skins;
using MyTools.UI.Objects.Buttons;
using UnityEngine;
using UnityEngine.UI;

namespace MyTools.Start.Buttons
{
    public class ShopLoaderButton : BaseButton
    {
        [Header("Shop Loader")]
        [SerializeField] private Image _iconImage;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Transform _transformParent;
        [SerializeField] private bool _loadProvider = true;
        [SerializeField] private bool _instance = false;

        private static ShopLoaderButton Instance;
        private ShopProvider _provider;

        private void Awake()
        {
            if (_instance && Instance == null)
            {
                Instance = this;
                _provider = new();
            }
        }

        protected override void OnButtonPressed()
        {
            base.OnButtonPressed();

            if (_loadProvider)
                Load();
            else
                Unload();
        }

        private void Load()
        {
            Instance._iconImage.enabled = false;
            Instance.DisableCanvasGroup();
            Instance.LoadShopView();
        }

        private void Unload()
        {
            Instance._iconImage.enabled = true;
            Instance.EnableCanvasGroup();
            Instance.UnloadShopView();
        }

        private async void LoadShopView() => await _provider.Load(_transformParent);
        private async void UnloadShopView() => await _provider.UnloadAsync();

        private void DisableCanvasGroup() => _canvasGroup.interactable = false;
        private void EnableCanvasGroup() => _canvasGroup.interactable = true;
    }
}