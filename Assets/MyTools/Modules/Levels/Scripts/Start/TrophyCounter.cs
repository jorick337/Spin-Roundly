using Cysharp.Threading.Tasks;
using MyTools.UI;

namespace MyTools.Levels.Start
{
    public class TrophyCounter : Counter
    {
        private LevelsManager _levelsManager;

        private async void Awake() 
        {
            _levelsManager = LevelsManager.Instance;
            await UniTask.WaitUntil(() => _levelsManager.IsLoaded);
            Initialize();
        } 

        private void OnEnable() => _levelsManager.TrophyChanged += UpdateTrophy;
        private void OnDisable() => _levelsManager.TrophyChanged -= UpdateTrophy;

        private void Initialize() => UpdateTrophy(_levelsManager.Trophy);
        private void UpdateTrophy(int trophy) => UpdateText(trophy.ToString());
    }
}