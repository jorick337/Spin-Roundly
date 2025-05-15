using UnityEngine;

namespace MyTools.Levels
{
    public class GameLevelView : MonoBehaviour
    {
        // Managers
        private LevelsManager _levelsManager;

        private void Awake() 
        {
           _levelsManager = LevelsManager.Instance; 
           _levelsManager.Load();
        } 
    }
}