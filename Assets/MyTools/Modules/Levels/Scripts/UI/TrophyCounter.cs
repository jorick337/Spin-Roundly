using MyTools.UI;

namespace MyTools.Levels.Start
{
    public class TrophyCounter : Counter
    {
        private LevelsManager _levelsManager;

        private async void Awake() 
        {
            _levelsManager = LevelsManager.Instance;
            await _levelsManager.WaitUntilLoaded();
            Initialize();
        }

        private void OnEnable() => _levelsManager.TrophiesChanged += UpdateTrophy;
        private void OnDisable() => _levelsManager.TrophiesChanged -= UpdateTrophy;

        private void Initialize() => UpdateTrophy(_levelsManager.Trophies);

        private void UpdateTrophy(int trophy) => UpdateText(trophy.ToString());
    }
}