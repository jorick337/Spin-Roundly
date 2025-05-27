using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using MyTools.UI;
using MyTools.UI.Animate;
using UnityEngine;

namespace MyTools.Levels.Start
{
    public class LevelsView_V1 : LevelsView
    {
        #region CORE

        [SerializeField] private AnimateAnchorPosInUI _animateAnchorPosInTitle;
        [SerializeField] private MyButton _closeButton;

        // Managers
        private LevelsViewProvider _levelsViewProvider;

        #endregion

        #region MONO

        private async void Start() => await AnimateAllIn();

        public override void OnEnable()
        {
            for (int i = 0; i < _levelButtons.Length; i++)
            {
                _levelButtons[i].OnPressed += DisableUIAndClick;
                _levelButtons[i].OnSelected += LoadLevelsScene;
            }
            _closeButton.OnPressed += DisableUIAndClick;
            _closeButton.OnPressEnded += DestroySelf;
        }

        public override void OnDisable()
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

        #endregion

        #region ANIMATIONS

        private async UniTask AnimateAll(Func<MyButton, UniTask> levelAnimation, Func<UniTask> titleAnimation, Func<UniTask> closeButtonAnimation)
        {
            var animations = new List<UniTask>();

            animations.AddRange(_levelButtons.Select(levelAnimation));
            animations.Add(titleAnimation());
            animations.Add(closeButtonAnimation());

            await UniTask.WhenAll(animations);
        }

        private async UniTask AnimateAllIn() =>
            await AnimateAll(x => x.AnimateScaleIn(), () => _animateAnchorPosInTitle.AnimateInAsync(), () => _closeButton.AnimateScaleIn());
        private async UniTask AnimateAllOut() =>
            await AnimateAll(x => x.AnimateScaleOut(), () => _animateAnchorPosInTitle.AnimateOutAsync(), () => _closeButton.AnimateScaleOut());

        #endregion

        #region CALLBACKS

        public override async UniTask DisableUIAndClick(AnimateScaleXInUI animateScaleXInUI)
        {
            DisableUI();
            PlayClickSound();
            await animateScaleXInUI.AnimateAsync();
            await AnimateAllOut();
        }

        private async UniTask DestroySelf() => await _levelsViewProvider.UnloadAsync();

        #endregion
    }
}