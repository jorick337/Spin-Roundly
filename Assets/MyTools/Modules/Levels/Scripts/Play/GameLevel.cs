using MyTools.UI;
using UnityEngine;

namespace MyTools.Levels.Play
{
    public class GameLevel : MonoBehaviour
    {
        #region  CORE

        [Header("Finish")]
        [SerializeField] private ColliderTrigger _colliderTrigger;

        private GameLevelsProvider _gameLevelsProvider;

        #endregion

        #region MONO

        private void OnEnable()
        {
            _colliderTrigger.OnTriggered += LoadVictoryMenu;
        }

        private void OnDisable()
        {
            _colliderTrigger.OnTriggered -= LoadVictoryMenu;
        }

        #endregion

        #region VALUES

        public void SetGameLevelsProvider(GameLevelsProvider gameLevelsProvider) => _gameLevelsProvider = gameLevelsProvider;

        #endregion

        #region CALLBACKS

        private void LoadVictoryMenu() => Debug.Log(1);

        #endregion
    }
}