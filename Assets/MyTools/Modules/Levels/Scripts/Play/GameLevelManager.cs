using UnityEngine;

namespace MyTools.Levels.Play
{
    public class GameLevelManager : MonoBehaviour
    {
        public static GameLevelManager Instance { get; private set; }

        // Managers
        private LevelsManager _levelsManager;

        private void Awake()
        {
            Instance = this;
            _levelsManager = LevelsManager.Instance;
        }

        private void Start() => _levelsManager.Load();
    }
}