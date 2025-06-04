using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace MyTools.Levels.Play
{
    public class GameLevelManager : MonoBehaviour
    {

        #region EVENTS

        public event UnityAction OnFinish;
        public event UnityAction OnRestart;

        public event UnityAction<int> StarsChanged;

        #endregion

        #region CORE

        public static GameLevelManager Instance { get; private set; }

        public int Stars { get; private set; } = 0;

        private GameLevel _gameLevel;
        private bool IsLoaded = false;

        // Managers
        private LevelsManager _levelsManager;

        #endregion

        #region MONO

        private void Awake()
        {
            Instance = this;
            _levelsManager = LevelsManager.Instance;
        }

        private void Start() => Load();

        #endregion

        #region CORE LOGIC

        public void Finish()
        {
            LoadVictoryView();
            InvokeOnFinish();
        }

        public void Restart()
        {
            ResetStars();
            InvokeOnRestart();
        }

        public void Next() => LoadNextLevel();

        #endregion

        #region LOAD

        public async UniTask WaitUntilLoaded() => await UniTask.WaitUntil(() => IsLoaded);

        private async void LoadVictoryView()
        {
            VictoryViewProvider victoryViewProvider = new();
            await victoryViewProvider.Load();
        }

        private async void LoadNextLevel()
        {
            await _gameLevel.Unload();
            _gameLevel = await _levelsManager.LoadNextLevel();
        }

        private async void Load() 
        {
            _gameLevel = await _levelsManager.Load();
            IsLoaded = true;
        } 

        #endregion

        #region VALUES

        public void AddStar()
        {
            Stars += 1;
            InvokeStarsChanged();
        }

        private void ResetStars() => Stars = 0;

        #endregion

        #region CALLBACKS

        private void InvokeOnFinish() => OnFinish?.Invoke();
        private void InvokeOnRestart() => OnRestart?.Invoke();

        private void InvokeStarsChanged() => StarsChanged?.Invoke(Stars);

        #endregion
    }
}