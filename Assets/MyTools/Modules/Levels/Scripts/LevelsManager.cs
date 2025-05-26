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
        public event UnityAction<int> TrophyChanged;

        #endregion

        #region CORE

        public static LevelsManager Instance { get; private set; }

        public int[] Stars { get; private set; }
        public int Trophy { get; private set; }

        public bool IsLoaded { get; private set; } = false;

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

        public void Initialize(int[] stars, int trophy)
        {
            Stars = stars;
            Trophy = trophy;
            IsLoaded = true;
        }

        public async void Load()
        {
            GameLevelsProvider gameLevelsProvider = new();
            GameLevel = await gameLevelsProvider.Load(_level, async () =>
            {
                GameLevel.StarsCollected -= SetStars;
                GameLevel.TrophyCollected -= AddTrophy;
                GameLevel.OnNextLevel -= LoadNextLevel;
                await UniTask.CompletedTask;
            });
            GameLevel.StarsCollected += SetStars;
            GameLevel.TrophyCollected += AddTrophy;
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

        private void AddTrophy(int trophy) 
        {
            Trophy += trophy;
            InvokeTrophyChanged();
        }

        #endregion

        #region CALLBACKS

        private void LoadNextLevel()
        {
            AddLevel();
            Load();
        }

        private void InvokeStarsChanged() => StarsChanged?.Invoke(Stars);
        private void InvokeTrophyChanged() => TrophyChanged?.Invoke(Trophy);

        #endregion
    }
}