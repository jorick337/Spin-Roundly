using System;
using Cysharp.Threading.Tasks;
using MyTools.UI;
using UnityEngine;

namespace MyTools.Levels
{
    public class LevelButton : MyButton
    {
        public Func<int, UniTask> OnSelected;

        [SerializeField] private int _level = 1;

        private void OnValidate()
        {
            if (transform.parent == null)
                return;

            LevelButton[] siblings = transform.parent.GetComponentsInChildren<LevelButton>(true);

            for (int i = 0; i < siblings.Length; i++)
            {
                if (siblings[i] == this)
                {
                    _level = i + 1;
                    break;
                }
            }
        }

        public override async void ClickAsync()
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