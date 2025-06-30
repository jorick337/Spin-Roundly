using MyTools.UI;
using UnityEngine;

namespace MyTools.Levels.TwoDimensional.Objects
{
    public class Trampoline_TRP2 : LevelItem
    {
        [Header("Trampoline")]
        [SerializeField] private float _bounsForce = 0f;

        protected override void DoActionBeforeRestart() { }
        protected override void DoActionOnAwake() { }

        protected override void InvokeTriggeredEnter(Collider2D collider2D)
        {
            Rigidbody2D rb2 = collider2D.attachedRigidbody;

            if (rb2 != null)
            {
                rb2.linearVelocity = new Vector2(rb2.linearVelocityX, 0);
                rb2.AddForce(Vector2.up * _bounsForce, ForceMode2D.Impulse);
            }
        }

        protected override void InvokeTriggeredExit(Collider2D collider2D) { }
        protected override void InvokeTriggeredStay(Collider2D collider2D) { }
    }
}