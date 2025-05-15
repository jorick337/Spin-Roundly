using System;
using Cysharp.Threading.Tasks;
using MyTools.LocalAddressables;
using UnityEngine;

namespace MyTools.Levels
{
    public class LevelsViewProvider : LocalAssetLoader
    {
        public async void Load(Transform transform, Func<UniTask> action = null)
        {
            LevelsView levelsView = await LoadInternal<LevelsView>("LevelsView", transform);
            levelsView.SetLevelsPanelProvider(this);
            if (action != null)
                AddEvent(action);
        }
    }
}