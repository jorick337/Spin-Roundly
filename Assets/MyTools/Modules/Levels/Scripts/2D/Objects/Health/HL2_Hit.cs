using MyTools.UI.Animation;
using MyTools.UI.CameraSystem;
using MyTools.UI.Objects;
using UnityEngine;

namespace MyTools.Levels.TwoDimensional.Objects.Health
{
    public class HL2_Hit : MonoBehaviour
    {
        [SerializeField] private Health_HL2 _health;
        [SerializeField] private CameraFollower _cameraFollower;
        [SerializeField] private CameraShaker _cameraShaker;
        [SerializeField] private StunAndInvincibility _stunAndInvincibility;
        [SerializeField] private AnimationTransparency _animationTransparency;

        private void OnEnable() => _health.OnChanged += Apply;
        private void OnDisable() => _health.OnChanged -= Apply;

        private void Apply()
        {
            Shake();
            Stun();
        }

        private async void Shake()
        {
            _cameraFollower.Disable();
            _cameraShaker.Shake();
            await _cameraShaker.WaitUntilFinished();
            _cameraFollower.Enable();
        }

        private async void Stun()
        {
            _stunAndInvincibility.Stun();
            _animationTransparency.StartAlwaysAnimation();
            await _stunAndInvincibility.WaitUntilFinished();
            _animationTransparency.StopAlwaysAnimation();
        }

        private void Apply(int health) => Apply(); 
    }
}