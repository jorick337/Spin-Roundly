using MyTools.UI;

namespace MyTools.Levels.Start
{
    public class StarsCounter : Counter
    {
        private LevelsManager _levelsManager;

        private async void Awake() 
        {
            _levelsManager = LevelsManager.Instance;
            await _levelsManager.WaitUntilLoaded();
            Initialize();
        } 

        private void OnEnable() => _levelsManager.StarsChanged += UpdateStars;
        private void OnDisable() => _levelsManager.StarsChanged -= UpdateStars;

        private void Initialize() => UpdateStars(_levelsManager.GetStars());

        private void UpdateStars(int stars) => UpdateText(stars.ToString());
        private void UpdateStars(int[] stars) => UpdateStars(_levelsManager.GetStars());
    }
}