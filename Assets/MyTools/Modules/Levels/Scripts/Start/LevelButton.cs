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
        [SerializeField] private Image[] _starImages = new Image[3];
        [SerializeField] private Material _starVisibleMaterial;
        [SerializeField] private Color _starVisibleColor;

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

        public void Initialize()
        {
            int stars = _levelsManager.Stars[_level - 1];
            for (int i = 0; i < stars; i++)
            {
                _starImages[i].material = _starVisibleMaterial;
                _starImages[i].color = _starVisibleColor;
            }
        }

        public override async void ClickButtonAsync()
        {
            await InvokeOnPressed();
            await InvokeOnPressEnded();
            await InvokeOnSelectedAsync();
        }

        private async UniTask InvokeOnSelectedAsync()
        {
            if (OnSelected != null)
                await OnSelected.Invoke(_level);
        }
    }
}