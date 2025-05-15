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