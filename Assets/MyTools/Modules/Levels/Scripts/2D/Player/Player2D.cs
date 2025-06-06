using MyTools.Levels.Play;
using MyTools.UI.Animation;
using MyTools.UI.CameraSystem;
using MyTools.UI.Objects;
using UnityEngine;

namespace MyTools.Levels.TwoDimensional.Player
{
    public class Player2D : MonoBehaviour
    {
        [SerializeField] private Health _health;
        [SerializeField] private StunAndInvincibility _stunAndInvincibility;
        [SerializeField] private AnimationTransparency _animationTransparency;
        [SerializeField] private CameraFollower _cameraFollower;
        [SerializeField] private CameraShaker _cameraShaker;

        private void OnEnable() => _health.Changed += Apply;
        private void OnDisable() => _health.Changed -= Apply;

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

        private void Apply(int heart) => Apply();
    }
}