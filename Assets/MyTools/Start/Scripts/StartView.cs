using System.Threading.Tasks;
using Game.UITools.Animate;
using MyTools.Music;
using MyTools.Settings;
using MyTools.UI.Animate;
using UnityEngine;
using UnityEngine.UI;

namespace MyTools.Start
{
    public class StartView : MonoBehaviour
    {
        #region  CORE

        [Header("Core")]
        [SerializeField] private AnimateTranparencyInUI _animateTransparencyInTitleText;

        [Header("Start")]
        [SerializeField] private Button _startButton;
        [SerializeField] private AnimateScaleInUI _animateScaleInStartButton;
        [SerializeField] private AnimateScaleXInUI _animateClickStartButton;

        [Header("Settings")]
        [SerializeField] private Button _settingsButton;
        [SerializeField] private AnimateScaleInUI _animateScaleInSettingsButton;
        [SerializeField] private AnimateScaleXInUI _animateClickSettingsButton;

        [Header("Shop")]
        [SerializeField] private Button _shopButton;
        [SerializeField] private AnimateScaleInUI _animateScaleInShopButton;
        [SerializeField] private AnimateScaleXInUI _animateClickShopButton;

        // Managers
        private MusicManager _musicManager;

        #endregion

        #region MONO

        private void Awake() => _musicManager = MusicManager.Instance;

        private async void Start() => await AnimateAllInAsync();

        private void OnEnable()
        {
            _startButton.onClick.AddListener(LoadLevelsPanel);
            _settingsButton.onClick.AddListener(LoadSettingsPanel);
            _shopButton.onClick.AddListener(LoadShopPanel);
        }

        private void OnDisable()
        {
            _startButton.onClick.RemoveListener(LoadLevelsPanel);
            _settingsButton.onClick.RemoveListener(LoadSettingsPanel);
            _shopButton.onClick.RemoveListener(LoadShopPanel);
        }

        #endregion

        #region UI

        private void SetInteractableButtons(bool active)
        {
            _startButton.interactable = active;
            _settingsButton.interactable = active;
            _shopButton.interactable = active;
        }

        private void DisableUI() => SetInteractableButtons(false);
        private void EnableButtons() => SetInteractableButtons(true);

        #endregion

        #region ANIMATIONS

        private async Task AnimateAllInAsync() => await Task.WhenAll(_animateTransparencyInTitleText.AnimateInAsync(), AnimateButtonsInAsync());
        private async Task AnimateAllOutAsync() => await Task.WhenAll(_animateTransparencyInTitleText.AnimateOutAsync(), AnimateButtonsOutAsync());

        private async Task AnimateButtonsInAsync() => await Task.WhenAll(_animateScaleInStartButton.AnimateInAsync(), _animateScaleInSettingsButton.AnimateInAsync());
        private async Task AnimateButtonsOutAsync() => await Task.WhenAll(_animateScaleInStartButton.AnimateOutAsync(), _animateScaleInSettingsButton.AnimateOutAsync());

        #endregion

        #region CALLBACKS

        private async void LoadLevelsPanel()
        {
            DisableUI();
            PlayClickSound();
            await _animateClickStartButton.AnimateAsync();
            await AnimateAllOutAsync();

            // LevelsPanelProvider levelsPanelProvider = new();
            // levelsPanelProvider.Load(transform.parent, async () =>
            // {
            //     await AnimateAllInAsync();
            //     EnableButtons();
            // });
        }

        private async void LoadSettingsPanel()
        {
            DisableUI();
            PlayClickSound();
            await _animateClickSettingsButton.AnimateAsync();
            await AnimateButtonsOutAsync();

            SettingsViewProvider settingsViewProvider = new();
            settingsViewProvider.Load(transform.parent, async () =>
            {
                await AnimateButtonsInAsync();
                EnableButtons();
            });
        }

        private async void LoadShopPanel()
        {
            DisableUI();
            PlayClickSound();
            await _animateClickStartButton.AnimateAsync();
            await AnimateAllOutAsync();
        }

        private void PlayClickSound() => _musicManager.PlayClickSound();

        #endregion
    }
}