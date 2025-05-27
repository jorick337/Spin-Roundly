using Cysharp.Threading.Tasks;
using MyTools.Music;
using MyTools.UI;
using MyTools.UI.Animation;
using UnityEngine;

namespace MyTools.Settings
{
    public class SettingsView : MonoBehaviour
    {
        #region CORE

        [Header("Core")]
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private AnimationAchorPos _animateAnchorPosInBackground;

        [Header("Close")]
        [SerializeField] private MyButton _closeButton;

        // Managers
        private MusicManager _musicManager;
        private SettingsViewProvider _settingsViewProvider;

        #endregion

        #region MONO

        private void Awake() => _musicManager = MusicManager.Instance;
        private async void Start() => await _animateAnchorPosInBackground.AnimateInAsync();
        private void OnEnable() => _closeButton.OnPressed += ClearSettingView;
        private void OnDisable() => _closeButton.OnPressed -= ClearSettingView;

        #endregion

        #region UI

        public void SetSettingsProvider(SettingsViewProvider settingsProvider) => _settingsViewProvider = settingsProvider;

        private void DisableUI() => _canvasGroup.interactable = false;

        #endregion

        #region CALLBACKS

        private async UniTask ClearSettingView(AnimationScaleX animationScaleX)
        {
            DisableUI();
            await animationScaleX.AnimateAsync();
            PlayClickSound();
            await _animateAnchorPosInBackground.AnimateOutAsync();
            await _settingsViewProvider.UnloadAsync();
        }

        private void PlayClickSound() => _musicManager.PlayClickSound();

        #endregion
    }
}