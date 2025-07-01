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

        protected override void DoActionBeforeRestart()
        {
            _animationMove.AnimateOut();
            _teleport.SendToTarget();
            _animationMove.AnimateIn();
        }

        protected override void DoActionOnAwake() => _animationMove.AnimateIn();
        protected override void InvokeTriggeredEnter(Collider2D collider2D) { }
        protected override void InvokeTriggeredExit(Collider2D collider2D) { }
        protected override void InvokeTriggeredStayAsync(Collider2D collider2D) { }
    }
}