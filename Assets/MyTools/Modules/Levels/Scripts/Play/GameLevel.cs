using Cysharp.Threading.Tasks;
using MyTools.Movement.TwoDimensional;
using MyTools.UI;
using UnityEngine;

namespace MyTools.Levels.Play
{
    public class GameLevel : MonoBehaviour
    {
        #region  CORE

        [Header("Finish")]
        [SerializeField] private Collider2DTrigger _finishColliderTrigger;
        [SerializeField] private Movement2D _movement2D;

        [Header("Restart")]
        [SerializeField] private Collider2DTrigger _defeatColliderTrigger;
        [SerializeField] private Teleport _teleportPlayer;

        // Managers
        private GameLevelManager _gameLevelManager;
        private GameLevelsProvider _gameLevelsProvider;

        #endregion

        #region MONO

        private void Awake() => _gameLevelManager = GameLevelManager.Instance;

        private void OnEnable()
        {
            _gameLevelManager.OnFinish += Finish;
            _gameLevelManager.OnRestart += Restart;
            _finishColliderTrigger.OnTriggered += InvokeFinish;
            _defeatColliderTrigger.OnTriggered += InvokeRestart;
        }

        private void OnDisable()
        {
            _gameLevelManager.OnFinish -= Finish;
            _gameLevelManager.OnRestart -= Restart;
            _finishColliderTrigger.OnTriggered -= InvokeFinish;
            _defeatColliderTrigger.OnTriggered -= InvokeRestart;
        }

        #endregion

        #region VALUES

        public void SetProvider(GameLevelsProvider gameLevelsProvider) => _gameLevelsProvider = gameLevelsProvider;
        public UniTask Unload() => _gameLevelsProvider.UnloadAsync();

        #endregion

        #region CALLBACKS

        private void Restart()
        {
            _teleportPlayer.SendToTarget();
            _movement2D.Enable();
        }
        
        private void Finish() => _movement2D.Disable();

        private void InvokeFinish(Collider2D collider2D) => _gameLevelManager.Finish();
        private void InvokeRestart(Collider2D collider2D) => _gameLevelManager.Restart();

        #endregion
    }
}