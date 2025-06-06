using Cysharp.Threading.Tasks;
using MyTools.Movement.TwoDimensional;
using MyTools.UI;
using UnityEngine;

namespace MyTools.Levels.Play
{
    public class GameLevel : MonoBehaviour
    {
        #region  CORE

        [Header("Core")]
        [SerializeField] private Movement2D _movement2D;
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
        }

        private void OnDisable()
        {
            _gameLevelManager.OnFinish -= Finish;
            _gameLevelManager.OnRestart -= Restart;
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

        #endregion
    }
}