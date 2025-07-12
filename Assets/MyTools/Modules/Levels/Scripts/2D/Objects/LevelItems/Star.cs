using MyTools.UI;
using MyTools.UI.Animation;
using UnityEngine;

namespace MyTools.Levels.Play
{
    public class Star : LevelItem
    {
        [Header("Star")]
        [SerializeField] private AnimationMove _animationMove;

        protected override void AutoAssignComponents()
        {
            base.AutoAssignComponents();
            if (_animationMove == null)
                _animationMove = GetComponent<AnimationMove>();
        }

        protected override void Restart()
        {
            base.Restart();
            _animationMove.StartAlwaysAnimation();
        }

        protected override void Enter(Collider2D collider2D)
        {
            _gameLevelManager.AddStar();
            _animationMove.StopAlwaysAnimation();
        }
    }
}