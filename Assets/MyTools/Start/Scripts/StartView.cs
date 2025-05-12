using Cysharp.Threading.Tasks;
using MyTools.Music;
using MyTools.Settings;
using MyTools.UI.Animate;
using UnityEngine;

namespace MyTools.Start
{
    public class StartView : MonoBehaviour
    {
        #region  CORE

        [Header("Core")]
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private AnimateTranparencyInUI _animateTransparencyInTitleText;

        [Header("Buttons")]
        [SerializeField] private StartButton _startButton;
        [SerializeField] private StartButton _settingsButton;
        [SerializeField] private StartButton _shopButton;

        private StartButton[] _startButtons;

        // Managers
        private MusicManager _musicManager;

        #endregion

        #region MONO

        private void Awake()
        {
            _musicManager = MusicManager.Instance;
            _startButtons = new StartButton[3] { _startButton, _settingsButton, _shopButton };
        }

        private async void Start() => await AnimateAllInAsync();

        private void OnEnable()
        {
            foreach (var startButton in _startButtons)
                startButton.OnPressed += ClearStartView;

            // _startButton.OnPressEnded += ;
            _settingsButton.OnPressEnded += LoadSettingsPanel;
            // _shopButton.OnPressEnded += ;
        }

        private void OnDisable()
        {
            foreach (var startButton in _startButtons)
                startButton.OnPressed -= ClearStartView;

            // _startButton.OnPressEnded -= ;
            _settingsButton.OnPressEnded -= LoadSettingsPanel;
            // _shopButton.OnPressEnded -= ;
        }

        #endregion

        #region UI

        private void SetInteractableButtons(bool active) => _canvasGroup.interactable = active;

        private void EnableButtons() => SetInteractableButtons(true);
        private void DisableButtons() => SetInteractableButtons(false);

        #endregion

        #region ANIMATIONS

        private async UniTask AnimateAllInAsync() => await UniTask.WhenAll(
            _animateTransparencyInTitleText.AnimateInAsync(),
            _startButton.AnimateScaleIn(),
            _settingsButton.AnimateScaleIn(),
            _shopButton.AnimateScaleIn());

        private async UniTask AnimateAllOutAsync() => await UniTask.WhenAll(
            _animateTransparencyInTitleText.AnimateOutAsync(),
            _startButton.AnimateScaleOut(),
            _settingsButton.AnimateScaleOut(),
            _shopButton.AnimateScaleOut());

        #endregion

        #region CALLBACKS

        private async void ClearStartView(AnimateScaleXInUI animateScaleXInUI)
        {
            DisableButtons();
            PlayClickSound();
            await animateScaleXInUI.AnimateAsync();
            await AnimateAllOutAsync();
        }

        private void LoadLevelsPanel()
        {
            // LevelsPanelProvider levelsPanelProvider = new();
            // levelsPanelProvider.Load(transform.parent, async () =>
            // {
            //     await AnimateAllInAsync();
            //     EnableButtons();
            // });
        }

        private void LoadSettingsPanel()
        {
            SettingsViewProvider settingsViewProvider = new();
            settingsViewProvider.Load(transform.parent, async () =>
            {
                await AnimateAllInAsync();
                EnableButtons();
            });
        }

        // private async void LoadShopPanel()
        // {
        // }

        private void PlayClickSound() => _musicManager.PlayClickSound();

        #endregion
    }
}