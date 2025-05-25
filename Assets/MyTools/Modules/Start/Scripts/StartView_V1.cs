using Cysharp.Threading.Tasks;
using MyTools.Levels.Start;
using MyTools.Settings;
using MyTools.UI;
using MyTools.UI.Animate;
using UnityEngine;

namespace MyTools.Start
{
    public class StartView_V1 : StartView
    {
        #region  CORE

        [Header("Animations")]
        [SerializeField] private AnimateTranparencyInUI _animateTransparencyInTitleText;

        [Header("Buttons")]
        [SerializeField] private MyButton _startButton;
        [SerializeField] private MyButton _settingsButton;

        #endregion

        #region MONO

        private async void Start() => await AnimateAllInAsync();

        private void OnEnable()
        {
            _startButton.OnPressed += ClearStartView;
            _startButton.OnPressEnded += LoadLevelsView;
            _settingsButton.OnPressed += ClearStartView;
            _settingsButton.OnPressEnded += LoadSettingsView;
        }

        private void OnDisable()
        {
            _startButton.OnPressed -= ClearStartView;
            _startButton.OnPressEnded -= LoadLevelsView;
            _settingsButton.OnPressed -= ClearStartView;
            _settingsButton.OnPressEnded -= LoadSettingsView;
        }

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

        private UniTask LoadLevelsView()
        {
            LevelsViewProvider levelsViewProvider = new();
            levelsViewProvider.Load(transform.parent, async () =>
            {
                await AnimateAllInAsync();
                EnableUI();
            });
            return UniTask.CompletedTask;
        }

        private UniTask LoadSettingsView()
        {
            SettingsViewProvider settingsViewProvider = new();
            settingsViewProvider.Load(transform.parent, async () =>
            {
                await AnimateAllInAsync();
                EnableUI();
            });
            return UniTask.CompletedTask;
        }

        #endregion
    }
}