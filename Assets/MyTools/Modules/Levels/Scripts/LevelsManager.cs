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

        public event UnityAction Loaded;

        #endregion

        #region CORE

        public static LevelsManager Instance { get; private set; }

        public int[] Stars { get; private set; }
        public int Trophies { get; private set; }

        public int ChosedNumberLevel { get; private set; } = 1;

        public UniTask Initialized => _initializedTcs.Task;

        private UniTaskCompletionSource<bool> _initializedTcs = new();
        private GameLevelsProvider _gameLevelsProvider = new();

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

        #region INITIALIZATION

        public void Initialize(int[] stars, int trophy)
        {
            Stars = stars;
            Trophies = trophy;
            _initializedTcs.TrySetResult(true);
            Loaded?.Invoke();
        }

        #endregion

        #region LOAD

        public async UniTask<GameLevel> Load() => await _gameLevelsProvider.Load(ChosedNumberLevel);

        #endregion

        #region VALUES

        public void AddStars(int stars)
        {
            if (Stars[ChosedNumberLevel - 1] >= stars)
                return;

            Stars[ChosedNumberLevel - 1] = stars;
            InvokeStarsChanged();
        }

        public void AddTrophy(int trophy)
        {
            Trophies += trophy;
            InvokeTrophiesChanged();
        }

        public void AddLevel()
        {
            if (ChosedNumberLevel + 1 <= Stars.Length)
                ChosedNumberLevel += 1;
        }

        public int GetStars()
        {
            int number = 0;
            for (int i = 0; i < Stars.Length; i++)
                number += Stars[i];
            return number;
        }

        public bool IsChosedNumberLevelMaximum() => ChosedNumberLevel == Stars.Length;

        public void SetLevel(int level) => ChosedNumberLevel = level;

        #endregion

        #region CALLBACKS

        private void InvokeStarsChanged() => StarsChanged?.Invoke(Stars);
        private void InvokeTrophiesChanged() => TrophiesChanged?.Invoke(Trophies);

        #endregion
    }
}