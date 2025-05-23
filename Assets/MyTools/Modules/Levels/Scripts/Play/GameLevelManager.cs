using UnityEngine;

namespace MyTools.Levels.Play
{
    public class GameLevelManager : MonoBehaviour
    {
        public static GameLevelManager Instance { get; private set; }

        public int NumberStars { get; private set; } = 0;

        // Managers
        private LevelsManager _levelsManager;

        private void Awake()
        {
            Instance = this;
            _levelsManager = LevelsManager.Instance;
        }

        private void Start() => _levelsManager.Load();

        public void AddStar() => NumberStars += 1;
        public void ResetStars() => NumberStars = 0;
    }
}