using Cysharp.Threading.Tasks;
using MyTools.Loading;
using MyTools.Music;
using UnityEngine;
using UnityEngine.UI;

namespace MyTools.Levels.Start
{
    public class LevelsView : MonoBehaviour
    {
        #region CORE

        [Header("Core")]
        [SerializeField] protected CanvasGroup _canvasGroup;
        [SerializeField] protected LevelButton[] _levelButtons;

        [Header("Moving")]
        [SerializeField] protected Button _leftButton;
        [SerializeField] protected Button _rightButton;

        protected int _multiplyLevel = 0;

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
                _levelButtons[i].OnPressed += DisableUIAsync;
                _levelButtons[i].OnSelected += LoadLevelsScene;
            }

            _leftButton.onClick.AddListener(PrevPage);
            _rightButton.onClick.AddListener(NextPage);
        }

        public virtual void OnDisable()
        {
            for (int i = 0; i < _levelButtons.Length; i++)
            {
                _levelButtons[i].OnPressed -= DisableUIAsync;
                _levelButtons[i].OnSelected -= LoadLevelsScene;
            }

            _leftButton.onClick.RemoveListener(PrevPage);
            _rightButton.onClick.RemoveListener(NextPage);
        }

        #endregion

        protected void Initialize()
        {
            int lenght = _levelButtons.Length;
            for (int i = 0; i < lenght; i++)
            {
                int level = i + 1 + lenght * _multiplyLevel;
                _levelButtons[i].Initialize(level);
            }
        }

        #region UI

        protected void Turn(int direction)
        {
            _multiplyLevel += direction;

            SetActiveLeftButton(_multiplyLevel != 0);
            SetActiveRightButton(_multiplyLevel != 1);

            Initialize();
        }

        protected void SetActiveRightButton(bool active) => _rightButton.gameObject.SetActive(active);
        protected void SetActiveLeftButton(bool active) => _leftButton.gameObject.SetActive(active);
        protected void DisableUI() => _canvasGroup.interactable = false;

        #endregion

        #region CALLBACKS

        public virtual async UniTask DisableUIAsync()
        {
            DisableUI();
            await UniTask.CompletedTask;
        }

        protected async UniTask LoadLevelsScene(int level)
        {
            _levelsManager.SetLevel(level);
            await _loadScene.LoadAsync();
        }

        protected void NextPage() => Turn(1);
        protected void PrevPage() => Turn(-1);

        #endregion
    }
}