using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using MyTools.Levels.Start;
using MyTools.UI;
using MyTools.UI.Animation;
using UnityEngine;

namespace MyTools.Levels.UI.View
{
    public class LevelsView_V1 : MonoBehaviour
    {
        #region CORE

        [SerializeField] private AnimationAchorPos _animateAnchorPosInTitle;
        [SerializeField] private MyButton _closeButton;

        // Managers
        private LevelsViewProvider _levelsViewProvider;

        #endregion

        #region MONO

        private async void Start() => await AnimateAllIn();

        // public override void OnEnable()
        // {
        // //     for (int i = 0; i < _levelButtons.Length; i++)
        // //     {
        // //         _levelButtons[i].OnPressed += DisableUIAsync;
        // //         _levelButtons[i].OnSelected += LoadLevelsScene;
        // //     }
        //     _closeButton.OnPressed += DisableUIAsync;
        //     _closeButton.OnPressEnded += DestroySelf;
        // }

        // public override void OnDisable()
        // {
        //     // for (int i = 0; i < _levelButtons.Length; i++)
        //     // {
        //     //     _levelButtons[i].OnPressed -= DisableUIAsync;
        //     //     _levelButtons[i].OnSelected -= LoadLevelsScene;
        //     // }
        //     _closeButton.OnPressed -= DisableUIAsync;
        //     _closeButton.OnPressEnded -= DestroySelf;
        // }

        #endregion

        #region UI

        public void SetLevelsPanelProvider(LevelsViewProvider levelsViewProvider) => _levelsViewProvider = levelsViewProvider;

        #endregion

        #region ANIMATIONS

        private async UniTask AnimateAll(Func<MyButton, UniTask> levelAnimation, Func<UniTask> titleAnimation, Func<UniTask> closeButtonAnimation)
        {
            var animations = new List<UniTask>();

            // animations.AddRange(_levelButtons.Select(levelAnimation));
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

        public async UniTask DisableUIAsync()
        {
            // DisableUI();
            await AnimateAllOut();
        }

        private async UniTask DestroySelf() => await _levelsViewProvider.UnloadAsync();

        #endregion
    }
}