using System;
using Cysharp.Threading.Tasks;
using MyTools.LocalAddressables;

namespace MyTools.Advertising
{
    public class AdvertisingViewProvider : LocalAssetLoader
    {
        public async UniTask<AdvertisingView> LoadAsync()
        {
            AdvertisingView advertisingView = await LoadInternal<AdvertisingView>("AdvertisingView");
            advertisingView.SetProvider(this);

            return advertisingView;
        }
    }
}