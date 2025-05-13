using MyTools.UI;
using UnityEngine;
using UnityEngine.Events;

namespace MyTools.Levels
{
    public class LevelButton : MyButton
    {
        public event UnityAction<int> OnSelected;

        [SerializeField] private int _indexLevel = 0;

        public override void Click()
        {
            InvokeOnPressed();
            InvokeOnPressEnded();
            InvokeOnSelected();
        }

        private void InvokeOnSelected() => OnSelected?.Invoke(_indexLevel);
    }
}