using Cysharp.Threading.Tasks;
using MyTools.LocalAddressables;

namespace MyTools.Shop.Skins
{
    public class SSHV_SkinProvider : LocalAssetLoader
    {
        public async UniTask<SSHV_Skin> Load(int number)
        {
            SSHV_Skin skinShop = await LoadObjectAsync<SSHV_Skin>($"SkinShop {number}");
            UnloadPreviousHandle();
            return skinShop;
        }
    }
}