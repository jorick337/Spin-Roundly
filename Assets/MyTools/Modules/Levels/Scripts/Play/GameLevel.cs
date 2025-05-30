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
        [SerializeField] private ColliderTrigger _finishColliderTrigger;
        [SerializeField] private Movement2D _movement2D;

        [Header("Restart")]
        [SerializeField] private ColliderTrigger _defeatColliderTrigger;
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
            _finishColliderTrigger.TriggerEnter2D += _gameLevelManager.Finish;
            _defeatColliderTrigger.TriggerEnter2D += _gameLevelManager.Restart;
        }

        private void OnDisable()
        {
            _gameLevelManager.OnFinish -= Finish;
            _gameLevelManager.OnRestart -= Restart;
            _finishColliderTrigger.TriggerEnter2D -= _gameLevelManager.Finish;
            _defeatColliderTrigger.TriggerEnter2D -= _gameLevelManager.Restart;
        }

        #endregion

        #region VALUES

        public void SetProvider(GameLevelsProvider gameLevelsProvider) => _gameLevelsProvider = gameLevelsProvider;
        public UniTask Unload() => _gameLevelsProvider.UnloadAsync();

        #endregion

        #region CALLBACKS

        private void Finish() => _movement2D.Disable();

        private void Restart()
        {
            _teleportPlayer.SendToTarget();
            _movement2D.Enable();
        }

        #endregion
    }
}