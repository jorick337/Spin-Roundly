using MyTools.UI;
using MyTools.UI.Animation;
using UnityEngine;

namespace MyTools.Levels.Play
{
    public class Coin : LevelItem
    {
        [Header("Coin")]
        [SerializeField] private AnimationMove _animationMove;

        public override void Enable()
        {
            base.Enable();
            _animationMove.Initialize();
            _animationMove.StartAlwaysAnimation();
        }

        public override void Disable()
        {
            base.Disable();
            _animationMove.StopAlwaysAnimation();
        }

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
            _gameLevelManager.AddMoney(1);
            _animationMove.StopAlwaysAnimation();
        } 
    }
}