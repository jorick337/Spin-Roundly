using MyTools.UI;
using UnityEngine;

namespace MyTools.Levels.Play
{
    public class Star : LevelItem
    {
        [Header("Star")]
        [SerializeField] private ParticleSystem _particleSystem;

        protected override void DoActionOnAwake() { }
        protected override void DoActionBeforeRestart() { }

        protected override void InvokeTriggeredEnter(Collider2D collider2D)
        {
            Debug.Log(1);
            _gameLevelManager.AddStar();
            _particleSystem.Play();
        }

        protected override void InvokeTriggeredStayAsync(Collider2D collider2D) { }
        protected override void InvokeTriggeredExit(Collider2D collider2D) { }
    }
}