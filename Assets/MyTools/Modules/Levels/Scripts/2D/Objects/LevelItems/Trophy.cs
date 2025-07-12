using MyTools.UI;
using MyTools.UI.Animation;
using UnityEngine;

namespace MyTools.Levels.Play
{
    public class Trophy : LevelItem
    {
        [Header("Trophy")]
        [SerializeField] private AnimationMove _animationMove; 

        private LevelsManager _levelsManager;

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

        protected override void Awake()
        {
            base.Awake();
            _levelsManager = LevelsManager.Instance;
        }

        protected override void Restart()
        {
            base.Restart();
            _animationMove.StartAlwaysAnimation();
        }

        protected override void Enter(Collider2D collider2D)
        {
            _levelsManager.AddTrophy(1);
            _animationMove.StopAlwaysAnimation();
        }
    }
}