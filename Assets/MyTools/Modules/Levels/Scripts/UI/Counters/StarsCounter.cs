using MyTools.UI.Objects;

namespace MyTools.Levels.UI.Counters
{
    public class StarsCounter : Counter
    {
        private LevelsManager _levelsManager;

        private async void Awake() 
        {
            _levelsManager = LevelsManager.Instance;
            await _levelsManager.Initialized;
            UpdateCounter();
        }

        private void OnEnable() => _levelsManager.StarsChanged += OnStarsChanged;
        private void OnDisable() => _levelsManager.StarsChanged -= OnStarsChanged;

        private void UpdateCounter()
        {
            int stars = _levelsManager?.GetStars() ?? 0;
            UpdateText(stars.ToString());
        }
        
        private void OnStarsChanged(int[] _) => UpdateCounter();
    }
}