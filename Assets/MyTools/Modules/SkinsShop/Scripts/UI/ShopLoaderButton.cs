using MyTools.Shop.Skins;
using MyTools.UI.Objects.Buttons;
using UnityEngine;

namespace MyTools.Start.Buttons
{
    public class ShopLoaderButton : BaseButton
    {
        [Header("Shop Loader")]
        [SerializeField] private bool _loadProvider = true;
        [SerializeField] private bool _instance = false;

        private static ShopProvider _provider;

        private void Awake() 
        {
            if (_instance)
                _provider = new();
        }

        protected override void OnButtonPressed()
        {
            base.OnButtonPressed();

            if (_loadProvider)
                Load();
            else
                Unload();
        }

        private async void Load() => await _provider.Load(transform.parent);
        private async void Unload() => await _provider.UnloadAsync();
    }
}