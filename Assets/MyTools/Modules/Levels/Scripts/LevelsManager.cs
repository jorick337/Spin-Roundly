using UnityEngine;

namespace MyTools.Levels
{
    public class LevelsManager : MonoBehaviour
    {
        public static LevelsManager Instance { get; private set; }

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

        public void Load()
        {
            GameLevelsProvider gameLevelsProvider = new();
            gameLevelsProvider.Load(_level);
        }
    }
}