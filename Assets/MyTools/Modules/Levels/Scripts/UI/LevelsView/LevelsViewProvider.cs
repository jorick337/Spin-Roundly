using System;
using Cysharp.Threading.Tasks;
using MyTools.LocalAddressables;
using UnityEngine;

namespace MyTools.Levels.Start
{
    public class LevelsViewProvider : LocalAssetLoader
    {
        public async void Load(Transform transform, Func<UniTask> action = null)
        {
            LevelsView_V1 levelsView = await LoadGameObjectAsync<LevelsView_V1>("LevelsView", transform);
            levelsView.SetLevelsPanelProvider(this);
            if (action != null)
                AddEvent(action);
        }
    }
}