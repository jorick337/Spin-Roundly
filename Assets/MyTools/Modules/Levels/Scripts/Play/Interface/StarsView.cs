using MyTools.UI.Animation;
using UnityEngine;

namespace MyTools.Levels.Play
{
    public class StarsView : MonoBehaviour
    {
        [SerializeField] private AnimationTransparency[] _animationTransparenciesStars;

        // Managers
        [SerializeField] private GameLevelManager _gameLevelManager;

        private void OnEnable() => _gameLevelManager = GameLevelManager.Instance;
    }
}