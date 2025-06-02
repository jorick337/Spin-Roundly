using MyTools.UI;
using UnityEngine;

namespace MyTools.Levels.Play
{
    public class Trophy : LevelItem
    {
        [Header("Trophy")]
        [SerializeField] private ParticleSystem _particleSystem;

        // Managers
        private LevelsManager _levelsManager;

        protected override void DoActionOnAwake() => _levelsManager = LevelsManager.Instance;
        protected override void DoActionBeforeRestart() { }

        protected override void InvokeTrigger2D(Collider2D collider2D)
        {
            _levelsManager.AddTrophy(1);
            _particleSystem.Play();
        }
    }
}