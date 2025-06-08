using System;
using Cysharp.Threading.Tasks;
using MyTools.LocalAddressables;

namespace MyTools.Advertising
{
    public class AdvertisingViewProvider : LocalAssetLoader
    {
        public async UniTask<AdvertisingView> LoadAsync(Func<UniTask> action)
        {
            AdvertisingView advertisingView = await LoadInternal<AdvertisingView>("AdvertisingView");
            advertisingView.SetProvider(this);
            AddEvent(action);

            return advertisingView;
        }
    }
}