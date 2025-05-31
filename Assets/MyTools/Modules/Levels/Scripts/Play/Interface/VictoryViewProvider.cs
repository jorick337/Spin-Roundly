using System;
using Cysharp.Threading.Tasks;
using MyTools.LocalAddressables;

namespace MyTools.Levels.Play
{
    public class VictoryViewProvider : LocalAssetLoader
    {
        public async UniTask<VictoryView> Load()
        {
            VictoryView victoryView = await LoadInternal<VictoryView>("VictoryView");
            victoryView.SetProvider(this);

            return victoryView;
        }
    }
}