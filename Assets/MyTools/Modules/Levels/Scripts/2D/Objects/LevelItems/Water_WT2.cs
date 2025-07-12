using MyTools.UI;
using MyTools.UI.Animation;
using UnityEngine;

namespace MyTools.Levels.TwoDimensional.Objects
{
    public class Water_WT2 : LevelItem
    {
        [Header("Water")]
        [SerializeField] private Teleport _teleport;
        [SerializeField] private AnimationMove _animationMove;

        protected override void Restart()
        {
            base.Restart();
            _animationMove.AnimateOut();
            _teleport.SendToTarget();
            _animationMove.AnimateIn();
        }

        protected override void Awake() 
        {
            base.Awake();
            _animationMove.AnimateIn();
        }
    }
}