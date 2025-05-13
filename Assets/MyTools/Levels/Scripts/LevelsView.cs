using System;
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

        private void Awake() => _musicManager = MusicManager.Instance;

        private async void Start() => await AnimateAllIn();

        private void OnEnable()
        {
            for (int i = 0; i < _levelButtons.Length; i++)
            {
                _levelButtons[i].OnPressed += ClearLevelsView;
                _levelButtons[i].OnSelected += SetAndLoadScene;
            }

            _closeButton.OnPressed += ClearLevelsView;
        }

        private void OnDisable()
        {
            for (int i = 0; i < _levelButtons.Length; i++)
            {
                _levelButtons[i].OnPressed -= ClearLevelsView;
                _levelButtons[i].OnSelected -= SetAndLoadScene;
            }

            _closeButton.OnPressed -= ClearLevelsView;
        }

        #endregion

        #region UI

        public void SetLevelsPanelProvider(LevelsViewProvider levelsViewProvider) => _levelsViewProvider = levelsViewProvider;

        private void DisableUI() => _canvasGroup.interactable = false;

        #endregion

        #region ANIMATIONS

        private async Task AnimateAll(Func<MyButton, UniTask> levelAnimation, Func<UniTask> titleAnimation, Func<UniTask> closeButtonAnimation) =>
            await UniTask.WhenAll(UniTask.WhenAll(_levelButtons.Select(levelAnimation)), titleAnimation(), closeButtonAnimation());
        private async UniTask AnimateAllIn() =>
            await AnimateAll(x => x.AnimateScaleIn(), () => animateAnchorPosInTitle.AnimateInAsync(), () => _closeButton.AnimateScaleIn());
        private async UniTask AnimateAllOut() =>
            await AnimateAll(x => x.AnimateScaleOut(), () => animateAnchorPosInTitle.AnimateOutAsync(), () => _closeButton.AnimateScaleOut());

        #endregion

        #region CALLBACKS

        private async void ClearLevelsView(AnimateScaleXInUI animateScaleXInUI)
        {
            DisableUI();
            PlayClickSound();
            await animateScaleXInUI.AnimateAsync();
            await AnimateAllOut();
            _levelsViewProvider.Unload();
        }

        private void SetAndLoadScene(int indexScene)
        {
            
            _loadScene.Load();
        }

        private void PlayClickSound() => _musicManager.PlayClickSound();

        #endregion
    }
}