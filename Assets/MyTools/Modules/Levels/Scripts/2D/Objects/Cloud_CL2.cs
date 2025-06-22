using Cysharp.Threading.Tasks;
using MyTools.UI;
using MyTools.UI.Animation;
using UnityEngine;

namespace MyTools.Levels.TwoDimensional.Objects
{
    public class Cloud_CL2 : LevelItem
    {
        [Header("Cloud")]
        [SerializeField] private AnimationTransparency _animationTransparency;
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private float _timeToDie;

        protected override void DoActionOnAwake() { }
        protected override void DoActionBeforeRestart() { }

        protected override async void InvokeTrigger2D(Collider2D collider2D)
        {
            _animationTransparency.StartAlwaysAnimation();
            await WaitTimeToDie();
            _animationTransparency.StopAlwaysAnimation();
            _particleSystem.Play();
            DisableColliderTrigger();
        }

        private async UniTask WaitTimeToDie() => await UniTask.WaitForSeconds(_timeToDie);


    }
}