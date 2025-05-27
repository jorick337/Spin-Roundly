using Cysharp.Threading.Tasks;
using MyTools.Loading;
using MyTools.Music;
using MyTools.UI.Animate;
using UnityEngine;

namespace MyTools.Levels.Start
{
    public class LevelsView : MonoBehaviour
    {
        #region CORE

        [Header("Core")]
        [SerializeField] protected CanvasGroup _canvasGroup;
        [SerializeField] protected LevelButton[] _levelButtons;

        // Managers
        protected MusicManager _musicManager;
        protected LevelsManager _levelsManager;
        protected LoadScene _loadScene;

        #endregion

        #region MONO

        private async void Awake()
        {
            _musicManager = MusicManager.Instance;
            _levelsManager = LevelsManager.Instance;
            _loadScene = LoadScene.Instance;

            await UniTask.WaitUntil(() => _levelsManager.IsLoaded);
            Initialize();
        }

        public virtual void OnEnable()
        {
            for (int i = 0; i < _levelButtons.Length; i++)
            {
                _levelButtons[i].OnPressed += DisableUIAndClick;
                _levelButtons[i].OnSelected += LoadLevelsScene;
            }
        }

        public virtual void OnDisable()
        {
            for (int i = 0; i < _levelButtons.Length; i++)
            {
                _levelButtons[i].OnPressed -= DisableUIAndClick;
                _levelButtons[i].OnSelected -= LoadLevelsScene;
            }
        }

        #endregion

        #region UI

        protected void Initialize()
        {
            for (int i = 0; i < _levelButtons.Length; i++)
                _levelButtons[i].Initialize();
        }

        protected void DisableUI() => _canvasGroup.interactable = false;

        #endregion

        #region CALLBACKS

        public virtual async UniTask DisableUIAndClick(AnimateScaleXInUI animateScaleXInUI)
        {
            DisableUI();
            PlayClickSound();
            await animateScaleXInUI.AnimateAsync();
        }

        protected async UniTask LoadLevelsScene(int level)
        {
            _levelsManager.SetLevel(level);
            await _loadScene.LoadAsync();
        }

        protected void PlayClickSound() => _musicManager.PlayClickSound();

        #endregion
    }
}