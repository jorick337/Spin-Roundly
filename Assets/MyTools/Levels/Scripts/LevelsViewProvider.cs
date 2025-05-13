using MyTools.LocalAddressables;
using UnityEngine;
using UnityEngine.Events;

namespace MyTools.Levels
{
    public class LevelsViewProvider : LocalAssetLoader
    {
        public event UnityAction OnUnloaded;

        public async void Load(Transform transform, UnityAction action)
        {
            LevelsView levelsView = await LoadInternal<LevelsView>("LevelsView", transform);
            levelsView.SetLevelsPanelProvider(this);
            SetSettingsPanelUnloaded(action);
        }

        public void Unload()
        {
            OnUnloaded?.Invoke();
            UnloadInternal();
        }

        private void SetSettingsPanelUnloaded(UnityAction action)
        {
            void InvokeSettingsPanelUnloaded()
            {
                action?.Invoke();
                OnUnloaded -= action;
            }

            OnUnloaded += InvokeSettingsPanelUnloaded;
        }
    }
}
