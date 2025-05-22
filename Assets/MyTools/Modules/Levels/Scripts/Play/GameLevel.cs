using MyTools.UI;
using UnityEngine;

namespace MyTools.Levels.Play
{
    public class GameLevel : MonoBehaviour
    {
        #region  CORE

        [Header("Core")]
        [SerializeField] private ColliderTrigger _finishColliderTrigger;
        [SerializeField] private ColliderTrigger _defeatColliderTrigger;

        // Managers
        private GameLevelsProvider _gameLevelsProvider;

        #endregion

        #region MONO

        private void OnEnable()
        {
            _finishColliderTrigger.OnTriggered += LoadVictoryView;
            _defeatColliderTrigger.OnTriggered += Restart;
        }

        private void OnDisable()
        {
            _finishColliderTrigger.OnTriggered -= LoadVictoryView;
            _defeatColliderTrigger.OnTriggered -= Restart;
        }

        #endregion

        #region UI



        #endregion

        #region VALUES

        public void SetGameLevelsProvider(GameLevelsProvider gameLevelsProvider) => _gameLevelsProvider = gameLevelsProvider;

        #endregion

        #region CALLBACKS

        private void LoadVictoryView()
        {
            VictoryViewProvider victoryViewProvider = new();
            victoryViewProvider.Load();
        }

        private void Restart()
        {
            Debug.Log(123);
        }

        #endregion
    }
}