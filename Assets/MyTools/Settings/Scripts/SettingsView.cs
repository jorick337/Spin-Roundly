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
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private AnimateAnchorPosInUI _animateAnchorPosInBackground;

        [Header("SwitcherActiveToggles")]
        [SerializeField] private SwitcherActiveToggle _switcherActiveMusicToggle;
        [SerializeField] private SwitcherActiveToggle _switcherActiveSoundsToggle;

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
            _switcherActiveMusicToggle.UpdateActivity(_musicManager.IsMusicActive);
            _switcherActiveSoundsToggle.UpdateActivity(_musicManager.IsSoundsActive);
        }

        private async void Start() => await _animateAnchorPosInBackground.AnimateInAsync();

        private void OnEnable()
        {
            _closeButton.onClick.AddListener(ClearSettingView);
            _switcherActiveMusicToggle.OnPressed += PlayClickSound;
            _switcherActiveMusicToggle.OnPressEnded += _musicManager.SetIsActiveMusic;
            _switcherActiveSoundsToggle.OnPressed += PlayClickSound;
            _switcherActiveSoundsToggle.OnPressEnded += _musicManager.SetIsActiveSounds;
        }

        private void OnDisable()
        {
            _closeButton.onClick.RemoveListener(ClearSettingView);
            _switcherActiveMusicToggle.OnPressed -= PlayClickSound;
            _switcherActiveMusicToggle.OnPressEnded -= _musicManager.SetIsActiveMusic;
            _switcherActiveSoundsToggle.OnPressed -= PlayClickSound;
            _switcherActiveSoundsToggle.OnPressEnded -= _musicManager.SetIsActiveSounds;
        }

        #endregion

        #region UI

        public void SetSettingsProvider(SettingsViewProvider settingsProvider) => _settingsViewProvider = settingsProvider;

        #endregion

        #region CALLBACKS

        private async void ClearSettingView()
        {
            _canvasGroup.interactable = false;
            await _animateClickCloseButton.AnimateAsync();
            PlayClickSound();
            await _animateAnchorPosInBackground.AnimateOutAsync();
            _settingsViewProvider.Unload();
        }

        private void PlayClickSound() => _musicManager.PlayClickSound();

        #endregion
    }
}