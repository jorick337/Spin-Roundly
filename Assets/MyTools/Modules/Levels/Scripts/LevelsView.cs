using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using MyTools.Loading;
using MyTools.Music;
using MyTools.UI;
using MyTools.UI.Animate;
using UnityEngine;

namespace MyTools.Levels
{
    public class LevelsView : MonoBehaviour
    {
        #region CORE

        [Header("Core")]
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private AnimateAnchorPosInUI animateAnchorPosInTitle;

        [Header("Buttons")]
        [SerializeField] private LevelButton[] _levelButtons;
        [SerializeField] private MyButton _closeButton;

        // Managers
        private MusicManager _musicManager;
        private LevelsViewProvider _levelsViewProvider;
        private LoadScene _loadScene;

        #endregion

        #region MONO

        private void Awake()
        {
            _musicManager = MusicManager.Instance;
            _loadScene = LoadScene.Instance;
        }

        private async void Start() => await AnimateAllIn();

        private void OnEnable()
        {
            for (int i = 0; i < _levelButtons.Length; i++)
            {
                _levelButtons[i].OnPressed += DisableUIAndClick;
                _levelButtons[i].OnSelected += LoadLevelsScene;
            }

            _closeButton.OnPressed += DisableUIAndClick;
            _closeButton.OnPressEnded += DestroySelf;
        }

        private void OnDisable()
        {
            for (int i = 0; i < _levelButtons.Length; i++)
            {
                _levelButtons[i].OnPressed -= DisableUIAndClick;
                _levelButtons[i].OnSelected -= LoadLevelsScene;
            }

            _closeButton.OnPressed -= DisableUIAndClick;
            _closeButton.OnPressEnded -= DestroySelf;
        }

        #endregion

        #region UI

        public void SetLevelsPanelProvider(LevelsViewProvider levelsViewProvider) => _levelsViewProvider = levelsViewProvider;

        private void DisableUI() => _canvasGroup.interactable = false;

        #endregion

        #region ANIMATIONS

        private async Task AnimateAll(Func<MyButton, UniTask> levelAnimation, Func<UniTask> titleAnimation, Func<UniTask> closeButtonAnimation)
        {
            var animations = new List<UniTask>();

            animations.AddRange(_levelButtons.Select(levelAnimation));
            animations.Add(titleAnimation());
            animations.Add(closeButtonAnimation());

            await UniTask.WhenAll(animations);
        }

        private async UniTask AnimateAllIn() =>
            await AnimateAll(x => x.AnimateScaleIn(), () => animateAnchorPosInTitle.AnimateInAsync(), () => _closeButton.AnimateScaleIn());
        private async UniTask AnimateAllOut() =>
            await AnimateAll(x => x.AnimateScaleOut(), () => animateAnchorPosInTitle.AnimateOutAsync(), () => _closeButton.AnimateScaleOut());

        #endregion

        #region CALLBACKS

        private async UniTask DisableUIAndClick(AnimateScaleXInUI animateScaleXInUI)
        {
            DisableUI();
            PlayClickSound();
            await animateScaleXInUI.AnimateAsync();
            await AnimateAllOut();
        }

        private async UniTask LoadLevelsScene(int level)
        {
            await _loadScene.LoadAsync();
        }

        private async UniTask DestroySelf() => await _levelsViewProvider.UnloadAsync();

        private void PlayClickSound() => _musicManager.PlayClickSound();

        #endregion
    }
}