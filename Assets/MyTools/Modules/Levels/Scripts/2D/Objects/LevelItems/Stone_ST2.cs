using MyTools.UI;
using UnityEngine;

namespace MyTools.Levels.TwoDimensional.Objects
{
    public class Stone_ST2 : LevelItem
    {
        [Header("Stone")]
        [SerializeField] private Teleport _teleport;

        protected override void DoActionBeforeRestart() => _teleport.SendToTarget();
        protected override void DoActionOnAwake() { }
        protected override void InvokeTriggeredEnter(Collider2D collider2D) { }
        protected override void InvokeTriggeredExit(Collider2D collider2D) { }
        protected override void InvokeTriggeredStayAsync(Collider2D collider2D) { }
    }
}