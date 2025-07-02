using MyTools.UI;
using UnityEngine;

namespace MyTools.Levels.TwoDimensional.Objects
{
    public class Raft_RF2 : LevelItem
    {
        [Header("Raft")]
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private Teleport _teleport;

        protected override void DoActionBeforeRestart() 
        {
            FreezePositionX();
            _teleport.SendToTarget();
        }

        protected override void DoActionOnAwake() { }

        protected override void InvokeTriggeredEnter(Collider2D collider2D) => UnfreezePositionX();
        protected override void InvokeTriggeredExit(Collider2D collider2D) { }
        protected override void InvokeTriggeredStay(Collider2D collider2D) { }

        private void FreezePositionX() => _rigidbody2D.constraints |= RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        private void UnfreezePositionX() => _rigidbody2D.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
    }
}