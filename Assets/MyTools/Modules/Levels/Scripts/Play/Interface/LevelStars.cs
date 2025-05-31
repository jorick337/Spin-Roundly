using MyTools.UI.Animation;
using UnityEngine;

namespace MyTools.Levels.Play
{
    public class LevelStars : MonoBehaviour
    {
        [SerializeField] private AnimationTransparency[] _animationTransparenciesStars;

        // Managers
        [SerializeField] private GameLevelManager _gameLevelManager;

        private void OnEnable() 
        {
            _gameLevelManager.StarsChanged += ShowStar;
            _gameLevelManager.OnRestart += Restart;
        }

        private void OnDisable()
        {
            _gameLevelManager.StarsChanged -= ShowStar;
            _gameLevelManager.OnRestart -= Restart;
        }

        private void Restart() 
        {
            for (int i = 0; i < 3; i++)
                _animationTransparenciesStars[i].AnimateOut();
        }
        
        private void ShowStar(int number) => _animationTransparenciesStars[number - 1].AnimateIn();
    }
}