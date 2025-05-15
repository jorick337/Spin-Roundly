using System;
using Cysharp.Threading.Tasks;
using MyTools.LocalAddressables;
using UnityEngine;

namespace MyTools.Settings
{
    public class SettingsViewProvider : LocalAssetLoader
    {
        public async void Load(Transform transform, Func<UniTask> action)
        {
            SettingsView settingsView = await LoadInternal<SettingsView>("SettingsView", transform);
            settingsView.SetSettingsProvider(this);
            if (action != null)
                AddEvent(action);
        }
    }
}