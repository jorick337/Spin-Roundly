using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using MyTools.PlayerSystem;
using UnityEngine;
using UnityEngine.Events;

namespace MyTools.Levels.Play
{
    public class GameLevelManager : MonoBehaviour
    {
        #region EVENTS

        public event UnityAction OnFinish;

        public event Func<UniTask> OnPreRestart;
        public event UnityAction OnRestart;

        public event UnityAction<int> StarsChanged;
        public event UnityAction<int> CollectedMoneyChanged;

        #endregion

        #region CORE

        public static GameLevelManager Instance { get; private set; }

        public int Stars { get; private set; } = 0;
        public int CollectedMoney { get; private set; } = 0;

        private GameLevel _gameLevel;
        private bool IsLoaded = false;

        // Managers
        private LevelsManager _levelsManager;
        private PlayerManager _playerManager;

        #endregion

        #region MONO

        private void Awake()
        {
            Instance = this;
            _levelsManager = LevelsManager.Instance;
            _playerManager = PlayerManager.Instance;
        }

        private async void Start()
        {
            await LoadLevel();
            IsLoaded = true;
        }

        #endregion

        #region CORE LOGIC

        public void Finish()
        {
            LoadVictoryView();
            SaveStars();
            InvokeOnFinish();
        }

        public async void RestartAsync()
        {
            await InvokeOnPreRestartAsync();
            Restart();
        }

        public void Restart()
        {
            ResetStars();
            InvokeOnRestart();
        }

        public void Next()
        {
            Restart();
            LoadNextLevel();
        }

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
            _levelsManager.AddLevel();
            await LoadLevel();
        }

        private async UniTask LoadLevel()
        {
            if (_gameLevel != null)
                await _gameLevel.Unload();
            _gameLevel = await _levelsManager.Load();
            ResetCollectedMoney();
        }

        #endregion

        #region VALUES

        public void AddStar()
        {
            Stars += 1;
            InvokeStarsChanged();
        }

        public void AddMoney(int money)
        {
            CollectedMoney += money;
            _playerManager.Player.AddMoney(money);
            InvokeCollectedMoneyChanged();
        }

        private void ResetCollectedMoney() => CollectedMoney = 0;
        private void ResetStars() => Stars = 0;
        private void SaveStars() => _levelsManager.AddStars(Stars);

        #endregion

        #region CALLBACKS

        private async UniTask InvokeOnPreRestartAsync()
        {
            foreach (Func<UniTask> handler in OnPreRestart.GetInvocationList().Cast<Func<UniTask>>())
                await handler();
        }

        private void InvokeOnRestart() => OnRestart?.Invoke();
        private void InvokeOnFinish() => OnFinish?.Invoke();
        private void InvokeStarsChanged() => StarsChanged?.Invoke(Stars);
        private void InvokeCollectedMoneyChanged() => CollectedMoneyChanged?.Invoke(CollectedMoney);

        #endregion
    }
}