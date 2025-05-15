using UnityEngine;

namespace MyTools.Levels
{
    public class GameLevel : MonoBehaviour
    {
        private GameLevelsProvider _gameLevelsProvider;

        public void SetGameLevelsProvider(GameLevelsProvider gameLevelsProvider) => _gameLevelsProvider = gameLevelsProvider;
    }
}