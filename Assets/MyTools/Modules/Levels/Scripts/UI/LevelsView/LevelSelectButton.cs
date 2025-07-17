using MyTools.Loading;
using MyTools.UI.Objects;
using MyTools.UI.Objects.Buttons;
using UnityEngine;
using UnityEngine.UI;

namespace MyTools.Levels.UI.View
{
    public class LevelSelectButton : BaseButton
    {
        [Header("Level")]
        [SerializeField] private PageNavigator _pageNavigator;
        [SerializeField] private Text _text;
        [SerializeField] private int _initialLevel = 1;

        [Header("Animation Stars")]
        [SerializeField] private Image[] _starImages = new Image[3];
        [SerializeField] private Material _starVisibleMaterial;
        [SerializeField] private Material _starHiddenMaterial;
        [SerializeField] private Color _starVisibleColor;
        [SerializeField] private Color _starHidenColor;

        private int _currentLevel = 0;
        private int _numberOfButtons = 0;

        // Managers
        private LevelsManager _levelsManager;
        private LoadScene _loadScene;

        #region MONO

        private void Awake()
        {
            _levelsManager = LevelsManager.Instance;
            _loadScene = LoadScene.Instance;
            _currentLevel = _initialLevel;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            _levelsManager.Loaded += Initialize;
            _pageNavigator.PageChanged += Initialize;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _levelsManager.Loaded -= Initialize;
            _pageNavigator.PageChanged -= Initialize;
        }

        protected override void OnValidate()
        {
            base.OnValidate();

            if (transform.parent == null)
                return;

            LevelSelectButton[] siblings = transform.parent.GetComponentsInChildren<LevelSelectButton>(true);
            _numberOfButtons = siblings.Length;

            for (int i = 0; i < _numberOfButtons; i++)
            {
                if (siblings[i] == this)
                {
                    _initialLevel = i + 1;
                    _currentLevel = _initialLevel;
                    break;
                }
            }
        }

        #endregion

        private void Initialize()
        {
            SetText(_currentLevel.ToString());
            UpdateActivityStars(_levelsManager.Stars[_currentLevel - 1]);
        }

        private void Initialize(int numberPage)
        {
            _currentLevel = _initialLevel + numberPage * _numberOfButtons;
            Initialize();
        }

        #region UI

        private void UpdateActivityStars(int stars)
        {
            for (int i = 0; i < 3; i++)
            {
                bool isVisible = i < stars;

                _starImages[i].material = isVisible ? _starVisibleMaterial : _starHiddenMaterial;
                _starImages[i].color = isVisible ? _starVisibleColor : _starHidenColor;
            }
        }

        private void SetText(string text) => _text.text = text;

        #endregion

        #region CALLBACKS

        protected override async void OnButtonPressed()
        {
            _levelsManager.SetLevel(_currentLevel);
            await _loadScene.LoadAsync();
        }

        #endregion
    }
}