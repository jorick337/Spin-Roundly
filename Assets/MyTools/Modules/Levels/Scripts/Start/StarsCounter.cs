using Cysharp.Threading.Tasks;
using MyTools.UI;

namespace MyTools.Levels.Start
{
    public class StarsCounter : Counter
    {
        private LevelsManager _levelsManager;

        private async void Awake() 
        {
            _levelsManager = LevelsManager.Instance;
            await UniTask.WaitUntil(() => _levelsManager.IsLoaded);
            Initialize();
        } 

        private void OnEnable() => _levelsManager.StarsChanged += UpdateStars;
        private void OnDisable() => _levelsManager.StarsChanged -= UpdateStars;

        private void Initialize() => UpdateStars(_levelsManager.Stars);

        private void UpdateStars(int[] stars)
        {
            int number = 0;
            for (int i = 0; i < stars.Length; i++)
                number += stars[i];
            UpdateText(number.ToString());
        }
    }
}