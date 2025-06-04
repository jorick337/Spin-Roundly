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
        public event UnityAction<int> TrophiesChanged;

        #endregion

        #region CORE

        public static LevelsManager Instance { get; private set; }
        public bool IsLoaded { get; private set; } = false;

        public int[] Stars { get; private set; }
        public int Trophies { get; private set; }

        private int _chosedLevel = 1;

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

        #region LOAD

        public async UniTask<GameLevel> Load()
        {
            GameLevelsProvider gameLevelsProvider = new();
            return await gameLevelsProvider.Load(_chosedLevel);
        }

        public async UniTask<GameLevel> LoadNextLevel()
        {
            AddLevel();
            return await Load();
        }

        #endregion

        #region INITIALIZATION

        public void Initialize(int[] stars, int trophy)
        {
            Stars = stars;
            Trophies = trophy;
            IsLoaded = true;
        }

        #endregion

        #region VALUES

        public void AddTrophy(int trophy)
        {
            Trophies += trophy;
            InvokeTrophiesChanged();
        }

        public void SetLevel(int level) => _chosedLevel = level;
        public async UniTask WaitUntilLoaded() => await UniTask.WaitUntil(() => IsLoaded);

        private void AddStars(int stars)
        {
            if (Stars[_chosedLevel - 1] == 3)
                return;

            Stars[_chosedLevel - 1] = stars;
            InvokeStarsChanged();
        }

        private void AddLevel()
        {
            if (_chosedLevel + 1 <= Stars.Length)
                _chosedLevel += 1;
        }

        #endregion

        #region CALLBACKS

        private void InvokeStarsChanged() => StarsChanged?.Invoke(Stars);
        private void InvokeTrophiesChanged() => TrophiesChanged?.Invoke(Trophies);

        #endregion
    }
}