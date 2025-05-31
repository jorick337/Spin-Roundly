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
        
        protected override void InvokeTrigger2D(Collider2D collider2D)
        {
            _gameLevelManager.AddStar();
            _particleSystem.Play();
        }
    }
}