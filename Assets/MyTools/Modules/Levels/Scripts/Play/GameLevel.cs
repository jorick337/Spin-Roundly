using Cysharp.Threading.Tasks;
using MyTools.Levels.TwoDimensional.Player;
using UnityEngine;

namespace MyTools.Levels.Play
{
    public class GameLevel : MonoBehaviour
    {
        #region  CORE

        [SerializeField] private Player_PL2 _player;

        // Managers
        private GameLevelManager _gameLevelManager;
        private GameLevelsProvider _provider;

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

        public void SetProvider(GameLevelsProvider provider) => _provider = provider;
        public UniTask Unload() => _provider.UnloadAsync();

        #endregion

        #region CALLBACKS

        private void Restart() => _player.Restart();
        private void Finish() => _player.Disable();

        #endregion
    }
}