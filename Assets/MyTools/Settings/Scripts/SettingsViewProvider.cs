using MyTools.LocalAddressables;
using UnityEngine;
using UnityEngine.Events;

namespace MyTools.Settings
{
    public class SettingsViewProvider : LocalAssetLoader
    {
        public UnityAction SettingsPanelUnloaded;

        public async void Load(Transform transform, UnityAction action)
        {
            SettingsView settingsView = await LoadInternal<SettingsView>("SettingsView", transform);
            settingsView.SetSettingsProvider(this);
            SetSettingsPanelUnloaded(action);
        }

        public void Unload()
        {
            SettingsPanelUnloaded?.Invoke();
            UnloadInternal();
        }

        private void SetSettingsPanelUnloaded(UnityAction action)
        {
            void InvokeSettingsPanelUnloaded()
            {
                action?.Invoke();
                SettingsPanelUnloaded -= action;
            }

            SettingsPanelUnloaded += InvokeSettingsPanelUnloaded;
        }
    }
}