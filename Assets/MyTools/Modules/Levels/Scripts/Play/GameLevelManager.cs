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
            _levelsManager.Load();
        }

        private void AddStar() => NumberStars += 1;
    }
}