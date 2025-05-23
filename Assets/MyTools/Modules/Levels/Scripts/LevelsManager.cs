using Cysharp.Threading.Tasks;
using MyTools.Levels.Play;
using UnityEngine;

namespace MyTools.Levels
{
    public class LevelsManager : MonoBehaviour
    {
        private const int MAX_LEVEL = 15;

        public static LevelsManager Instance { get; private set; }

        // Managers
        private GameLevel _gameLevel;

        private int _level = 1;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void SetLevel(int level) => _level = level;

        private void AddLevel()
        {
            if (_level + 1 <= MAX_LEVEL)
                _level += 1;
        }

        public async void Load()
        {
            GameLevelsProvider gameLevelsProvider = new();
            _gameLevel = await gameLevelsProvider.Load(_level, async () => 
            {
                _gameLevel.OnNextLevel -= LoadNextLevel;
                await UniTask.CompletedTask;
            });
            _gameLevel.OnNextLevel += LoadNextLevel;
        }

        private void LoadNextLevel()
        {
            AddLevel();
            Load();
        }
    }
}