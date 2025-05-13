using Cysharp.Threading.Tasks;
using MyTools.Music;
using MyTools.Settings;
using MyTools.UI;
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
        [SerializeField] private MyButton _startButton;
        [SerializeField] private MyButton _settingsButton;
        [SerializeField] private MyButton _shopButton;

        // Managers
        private MusicManager _musicManager;

        #endregion

        #region MONO

        private void Awake() =>_musicManager = MusicManager.Instance;
        private async void Start() => await AnimateAllInAsync();

        private void OnEnable()
        {
            _startButton.OnPressed += ClearStartView;
            _settingsButton.OnPressed += ClearStartView;
            _shopButton.OnPressed += ClearStartView;

            // _startButton.OnPressEnded += ;
            _settingsButton.OnPressEnded += LoadSettingsPanel;
            // _shopButton.OnPressEnded += ;
        }

        private void OnDisable()
        {
            _startButton.OnPressed -= ClearStartView;
            _settingsButton.OnPressed -= ClearStartView;
            _shopButton.OnPressed -= ClearStartView;

            // _startButton.OnPressEnded -= ;
            _settingsButton.OnPressEnded -= LoadSettingsPanel;
            // _shopButton.OnPressEnded -= ;
        }

        #endregion

        #region UI

        private void EnableUI() => _canvasGroup.interactable = true;
        private void DisableUI() => _canvasGroup.interactable = false;

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
            DisableUI();
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
                EnableUI();
            });
        }

        // private async void LoadShopPanel()
        // {
        // }

        private void PlayClickSound() => _musicManager.PlayClickSound();

        #endregion
    }
}