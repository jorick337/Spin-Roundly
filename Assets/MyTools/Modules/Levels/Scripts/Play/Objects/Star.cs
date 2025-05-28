using MyTools.UI;
using UnityEngine;

namespace MyTools.Levels.Play
{
    public class Star : MapItem
    {
        [Header("Star")]
        [SerializeField] private ParticleSystem _particleSystem;

        protected override void ActivateTriggerEnter2D()
        {
            _gameLevel.AddStar();
            _particleSystem.Play();
        }

        protected override void ActivateCollisionEnter2D(Collision2D collision2D) { }

        protected override void DoActionBeforeRestart() { }
    }
}