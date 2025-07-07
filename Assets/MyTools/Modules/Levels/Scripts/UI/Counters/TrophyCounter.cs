using MyTools.UI.Objects;

namespace MyTools.Levels.UI.Counters
{
    public class TrophyCounter : Counter
    {
        private LevelsManager _levelsManager;

        private async void Awake() 
        {
            _levelsManager = LevelsManager.Instance;
            await _levelsManager.WaitUntilLoaded();
            UpdateCounter();
        }

        private void OnEnable() => _levelsManager.TrophiesChanged += OnTrophiesChanged;
        private void OnDisable() => _levelsManager.TrophiesChanged -= OnTrophiesChanged;

        private void UpdateCounter()
        {
            int trophies = _levelsManager?.Trophies ?? 0;
            UpdateText(trophies.ToString());
        }
        
        private void OnTrophiesChanged(int _) => UpdateCounter();
    }
}