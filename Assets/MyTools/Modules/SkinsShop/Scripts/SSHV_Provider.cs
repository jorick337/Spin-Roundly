using Cysharp.Threading.Tasks;
using MyTools.LocalAddressables;

namespace MyTools.Shop.Skins
{
    public class SSHV_Provider : LocalAssetLoader
    {
        public async UniTask Load()
        {
            SkinsShopView_SSHV shopView = await LoadGameObjectAsync<SkinsShopView_SSHV>("SSHV");
            shopView.SetProvider(this);
        }
    }
}