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

        #endregion

        #region  CORE

        [Header("Finish")]
        [SerializeField] private ColliderTrigger _finishColliderTrigger;
        [SerializeField] private Movement2D _movement2D;

        [Header("Restart")]
        [SerializeField] private ColliderTrigger _defeatColliderTrigger;
        [SerializeField] private Teleport _teleportPlayer;

        // Managers
        private GameLevelsProvider _gameLevelsProvider;

        #endregion

        #region MONO

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

        private void LoadVictoryView()
        {
            VictoryViewProvider victoryViewProvider = new();
            victoryViewProvider.Load();
        }

        #endregion

        #region CALLBACKS

        private void Finish()
        {
            _movement2D.Disable();
            LoadVictoryView();
        }

        private void Restart()
        {
            InvokeOnReload();
            _teleportPlayer.SendToTarget();
        }

        private void InvokeOnReload() => OnReload?.Invoke();

        #endregion
    }
}