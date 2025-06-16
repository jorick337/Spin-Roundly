using Cysharp.Threading.Tasks;
using MyTools.LocalAddressables;
using UnityEngine;

namespace MyTools.Shop.Skins
{
    public class SSHV_Provider : LocalAssetLoader
    {
        public async UniTask Load(Transform transform)
        {
            SkinsShopView_SSHV shopView = await LoadGameObjectAsync<SkinsShopView_SSHV>("SSHV", transform);
            shopView.SetProvider(this);
        }
    }
}