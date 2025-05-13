using MyTools.Loading;
using MyTools.UI;
using UnityEngine;
using UnityEngine.Events;

namespace MyTools.Levels
{
    public class LevelButton : MyButton
    {
        public event UnityAction<int> OnSelected;

        [Header("Scene")]
        [SerializeField] private GameScenes gameScenes;

        public override void Click()
        {
            InvokeOnPressed();
            InvokeOnPressEnded();
            InvokeOnSelected();
        }

        private void InvokeOnSelected() => OnSelected?.Invoke((int)gameScenes.IndexScene);
    }
}