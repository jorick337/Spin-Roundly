using Cysharp.Threading.Tasks;
using MyTools.Levels.Play;
using UnityEngine;
using UnityEngine.Events;

namespace MyTools.Levels
{
    public class LevelsManager : MonoBehaviour
    {
        #region EVENTS

        public event UnityAction<int[]> StarsChanged;

        #endregion

        #region CORE

        public static LevelsManager Instance { get; private set; }
        public int[] Stars { get; private set; }

        private int _level = 1;

        // Managers
        public GameLevel GameLevel { get; private set; }

        #endregion

        #region MONO

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(gameObject);
        }

        #endregion

        #region CORE LOGIC

        public void Initialize(int[] stars) => Stars = stars;

        public async void Load()
        {
            GameLevelsProvider gameLevelsProvider = new();
            GameLevel = await gameLevelsProvider.Load(_level, async () =>
            {
                GameLevel.OnStarsCollected -= SetStars;
                GameLevel.OnNextLevel -= LoadNextLevel;
                await UniTask.CompletedTask;
            });
            GameLevel.OnStarsCollected += SetStars;
            GameLevel.OnNextLevel += LoadNextLevel;
        }

        #endregion

        #region VALUES

        public void SetLevel(int level) => _level = level;
        public int GetStars() => Stars[_level - 1];

        private void AddLevel()
        {
            if (_level + 1 <= Stars.Length)
                _level += 1;
        }

        private void SetStars(int stars)
        {
            if (Stars[_level - 1] == 3)
                return;
                
            Stars[_level - 1] = stars;
            InvokeStarsChanged();
        }

        #endregion

        #region CALLBACKS

        private void LoadNextLevel()
        {
            AddLevel();
            Load();
        }

        private void InvokeStarsChanged() => StarsChanged?.Invoke(Stars);

        #endregion
    }
}