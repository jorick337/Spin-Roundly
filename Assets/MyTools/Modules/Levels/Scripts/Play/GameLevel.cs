using Cysharp.Threading.Tasks;
using MyTools.Loading;
using MyTools.Movement.TwoDimensional;
using MyTools.UI;
using UnityEngine;
using UnityEngine.Events;

namespace MyTools.Levels.Play
{
    public class GameLevel : MonoBehaviour
    {
        #region EVENTS

        public event UnityAction OnReload;
        public event UnityAction OnNextLevel;

        public event UnityAction<int> StarsCollected;
        public event UnityAction<int> TrophyCollected;

        #endregion

        #region  CORE

        [Header("Finish")]
        [SerializeField] private ColliderTrigger _finishColliderTrigger;
        [SerializeField] private Movement2D _movement2D;

        [Header("Restart")]
        [SerializeField] private ColliderTrigger _defeatColliderTrigger;
        [SerializeField] private Teleport _teleportPlayer;

        public int Stars { get; private set; } = 0;

        // Managers
        private VictoryView _victoryView;
        private LoadScene _loadScene;
        private GameLevelsProvider _gameLevelsProvider;

        #endregion

        #region MONO

        private void Awake() => _loadScene = LoadScene.Instance;

        private void OnEnable()
        {
            _finishColliderTrigger.OnTriggered += Finish;
            _defeatColliderTrigger.OnTriggered += Restart;
        }

        private void OnDisable()
        {
            _finishColliderTrigger.OnTriggered -= Finish;
            _defeatColliderTrigger.OnTriggered -= Restart;
        }

        #endregion

        #region VALUES

        public void SetGameLevelsProvider(GameLevelsProvider gameLevelsProvider) => _gameLevelsProvider = gameLevelsProvider;

        public void AddStar() => Stars += 1;
        public void ResetStars() => Stars = 0;

        private async void LoadVictoryView()
        {
            VictoryViewProvider victoryViewProvider = new();
            _victoryView = await victoryViewProvider.Load(async () =>
            {
                _victoryView.OnReloadPressed -= Restart;
                _victoryView.OnHomePressed -= LoadStartScene;
                _victoryView.OnForwardPressed -= LoadNextLevel;
                await UniTask.CompletedTask;
            });
            _victoryView.OnReloadPressed += Restart;
            _victoryView.OnHomePressed += LoadStartScene;
            _victoryView.OnForwardPressed += LoadNextLevel;
        }

        #endregion

        #region CALLBACKS

        private void Finish()
        {
            _movement2D.Disable();
            LoadVictoryView();
            InvokeOnStarsCollected();
        }

        private void Restart()
        {
            InvokeOnReload();
            _teleportPlayer.SendToTarget();
            _movement2D.Enable();
            ResetStars();
        }

        private async void LoadNextLevel()
        {
            InvokeOnNextLevel();
            await _gameLevelsProvider.UnloadAsync();
        }

        private void LoadStartScene() => _loadScene.Load();

        private void InvokeOnReload() => OnReload?.Invoke();
        private void InvokeOnNextLevel() => OnNextLevel?.Invoke();
        private void InvokeOnStarsCollected() => StarsCollected?.Invoke(Stars);

        #endregion
    }
}