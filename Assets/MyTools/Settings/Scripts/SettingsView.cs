using Game.Localization;
using MyTools.Music;
using MyTools.UI.Animate;
using UnityEngine;
using UnityEngine.UI;

namespace MyTools.Settings
{
    public class SettingsView : MonoBehaviour
    {
        #region CORE

        [Header("Core")]
        [SerializeField] private AnimateAnchorPosInUI _animateAnchorPosInBackground;

        [Header("Music")]
        [SerializeField] private Toggle _musicToggle;
        [SerializeField] private ToggleTextLocalization _toggleMusicTextLocalization;
        [SerializeField] private AnimateScaleXInUI _animateScaleXInMusicToggle;

        [Header("Sounds")]
        [SerializeField] private Toggle _soundsToggle;
        [SerializeField] private ToggleTextLocalization _toggleSoundsTextLocalization;
        [SerializeField] private AnimateScaleXInUI _animateScaleXInSoundsToggle;

        [Header("Close")]
        [SerializeField] private Button _closeButton;
        [SerializeField] private AnimateScaleXInUI _animateClickCloseButton;

        // Managers
        private MusicManager _musicManager;
        private SettingsViewProvider _settingsViewProvider;

        #endregion

        #region MONO

        private void Awake()
        {
            _musicManager = MusicManager.Instance;
            UpdateActivities();
        }

        private async void Start() => await _animateAnchorPosInBackground.AnimateInAsync();

        private void OnEnable()
        {
            _closeButton.onClick.AddListener(AnimatedSelfDestruction);
            _musicToggle.onValueChanged.AddListener(SwitchActiveMusic);
            _soundsToggle.onValueChanged.AddListener(SwitchActiveSounds);
        }

        private void OnDisable()
        {
            _musicToggle.onValueChanged.RemoveListener(SwitchActiveMusic);
            _soundsToggle.onValueChanged.RemoveListener(SwitchActiveSounds);
        }

        #endregion

        #region UI

        public void SetSettingsProvider(SettingsViewProvider settingsProvider) => _settingsViewProvider = settingsProvider;

        private void UpdateActivities()
        {
            _musicToggle.isOn = _musicManager.IsMusicActive;
            _toggleMusicTextLocalization.SetToggleState(_musicManager.IsMusicActive);
            _soundsToggle.isOn = _musicManager.IsSoundsActive;
            _toggleSoundsTextLocalization.SetToggleState(_musicManager.IsSoundsActive);
        }

        private void DisableUI() 
        {
            _closeButton.interactable = false;
            _musicToggle.interactable = false;
            _soundsToggle.interactable = false;
        }

        #endregion

        #region CALLBACKS

        private async void AnimatedSelfDestruction()
        {
            DisableUI();
            await _animateClickCloseButton.AnimateAsync();
            PlayClickSound();
            await _animateAnchorPosInBackground.AnimateOutAsync();
            _settingsViewProvider.Unload();
        }

        private void SwitchActiveMusic(bool isMusicActive)
        {
            _animateScaleXInMusicToggle.Animate();
            _musicManager.SetIsActiveMusic(isMusicActive);
            _toggleMusicTextLocalization.SetToggleState(isMusicActive);
        }

        private void SwitchActiveSounds(bool isSoundsActive)
        {
            _animateScaleXInSoundsToggle.Animate();
            _musicManager.SetIsActiveSounds(isSoundsActive);
            _toggleSoundsTextLocalization.SetToggleState(isSoundsActive);
        }

        private void PlayClickSound() => _musicManager.PlayClickSound();

        #endregion
    }
}