using Cysharp.Threading.Tasks;
using MyTools.Levels.Start;
using MyTools.Music;
using MyTools.Settings;
using MyTools.UI;
using MyTools.UI.Animate;
using UnityEngine;

namespace MyTools.Start
{
    public class StartView_V1 : MonoBehaviour
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

            _startButton.OnPressEnded += LoadLevelsPanel;
            _settingsButton.OnPressEnded += LoadSettingsPanel;
            // _shopButton.OnPressEnded += ;
        }

        private void OnDisable()
        {
            _startButton.OnPressed -= ClearStartView;
            _settingsButton.OnPressed -= ClearStartView;
            _shopButton.OnPressed -= ClearStartView;

            _startButton.OnPressEnded -= LoadLevelsPanel;
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

        private async UniTask ClearStartView(AnimateScaleXInUI animateScaleXInUI)
        {
            DisableUI();
            PlayClickSound();
            await animateScaleXInUI.AnimateAsync();
            await AnimateAllOutAsync();
        }

        private UniTask LoadLevelsPanel()
        {
            LevelsViewProvider levelsViewProvider = new();
            levelsViewProvider.Load(transform.parent, async () =>
            {
                await AnimateAllInAsync();
                EnableUI();
            });
            return UniTask.CompletedTask;
        }

        private UniTask LoadSettingsPanel()
        {
            SettingsViewProvider settingsViewProvider = new();
            settingsViewProvider.Load(transform.parent, async () =>
            {
                await AnimateAllInAsync();
                EnableUI();
            });
            return UniTask.CompletedTask;
        }

        // private async void LoadShopPanel()
        // {
        // }

        private void PlayClickSound() => _musicManager.PlayClickSound();

        #endregion
    }
}