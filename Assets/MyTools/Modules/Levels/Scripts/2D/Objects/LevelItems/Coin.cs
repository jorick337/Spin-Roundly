using MyTools.PlayerSystem;
using MyTools.UI;
using UnityEngine;

namespace MyTools.Levels.Play
{
    public class Coin : LevelItem
    {
        [Header("Coin")]
        [SerializeField] private ParticleSystem _particleSystem;

        protected override void DoActionOnAwake() { }
        protected override void DoActionBeforeRestart() { }

        protected override void InvokeTriggeredEnter(Collider2D collider2D)
        {
            _gameLevelManager.AddMoney(1);
            _particleSystem.Play();
        }

        protected override void InvokeTriggeredStay(Collider2D collider2D) { }

        protected override void InvokeTriggeredExit(Collider2D collider2D) { }
    }
}