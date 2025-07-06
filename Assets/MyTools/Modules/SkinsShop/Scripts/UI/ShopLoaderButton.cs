using MyTools.Shop.Skins;
using MyTools.UI.Objects.Buttons;

namespace MyTools.Start.Buttons
{
    public class ShopButton : BaseButton
    {
        private ShopProvider _provider;

        private void Awake() => _provider = new();

        protected override void OnButtonPressed()
        {
            base.OnButtonPressed();
            Load();
        }

        private async void Load() => await _provider.Load(transform.parent);
    }
}