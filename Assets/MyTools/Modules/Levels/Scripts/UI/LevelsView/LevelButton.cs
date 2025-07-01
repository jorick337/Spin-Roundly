using System;
using Cysharp.Threading.Tasks;
using MyTools.UI;
using UnityEngine;
using UnityEngine.UI;

namespace MyTools.Levels.Start
{
    public class LevelButton : MyButton
    {
        public Func<int, UniTask> OnSelected;

        [Header("Level")]
        [SerializeField] private int _level = 1;
        [SerializeField] private Text _text;
        [SerializeField] private Image[] _starImages = new Image[3];
        [SerializeField] private Material _starVisibleMaterial;
        [SerializeField] private Color _starVisibleColor;
        [SerializeField] private Color _starHidenColor;

        // Managers
        private LevelsManager _levelsManager;

        private void Awake() => _levelsManager = LevelsManager.Instance;

        public override void OnValidate()
        {
            if (transform.parent == null)
                return;

            LevelButton[] siblings = transform.parent.GetComponentsInChildren<LevelButton>(true);
            for (int i = 0; i < siblings.Length; i++)
                if (siblings[i] == this)
                {
                    _level = i + 1;
                    break;
                }

            Validate();
        }

        public void Initialize(int level)
        {
            SetLevel(level);
            UpdateActivityStars(_levelsManager.Stars[_level - 1]);
        }

        private void UpdateActivityStars(int stars)
        {
            for (int i = 0; i < 3; i++)
            {
                if (i < stars)
                {
                    _starImages[i].material = _starVisibleMaterial;
                    _starImages[i].color = _starVisibleColor;
                }
                else
                {
                    _starImages[i].material = null;
                    _starImages[i].color = _starHidenColor;
                }
            }
        }

        private void SetLevel(int level)
        {
            _level = level;
            SetText(level.ToString());
        }

        private void SetText(string text) => _text.text = text;

        #region CALLBACKS

        public override async void ClickButtonAsync()
        {
            PlayClickSound();
            await AnimateClickAsync();

            await InvokeOnPressed();
            await InvokeOnPressEnded();
            await InvokeOnSelectedAsync();
        }

        private async UniTask InvokeOnSelectedAsync()
        {
            if (OnSelected != null)
                await OnSelected.Invoke(_level);
        }

        #endregion
    }
}