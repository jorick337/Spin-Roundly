using System;
using Cysharp.Threading.Tasks;
using MyTools.LocalAddressables;

namespace MyTools.Levels.Play
{
    public class VictoryViewProvider : LocalAssetLoader
    {
        public async UniTask<VictoryView> Load(Func<UniTask> action)
        {
            VictoryView victoryView = await LoadInternal<VictoryView>("VictoryView");
            victoryView.SetProvider(this);
            AddEvent(action);

            return victoryView;
        }
    }
}