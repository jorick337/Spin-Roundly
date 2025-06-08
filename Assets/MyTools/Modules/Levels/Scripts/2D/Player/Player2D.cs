using Cysharp.Threading.Tasks;
using MyTools.Advertising;
using MyTools.Levels.Play;
using MyTools.UI.Animation;
using MyTools.UI.CameraSystem;
using MyTools.UI.Objects;
using UnityEngine;

namespace MyTools.Levels.TwoDimensional.Player
{
    public class Player2D : MonoBehaviour
    {
        #region CORE

        [Header("Core")]
        [SerializeField] private Health _health;

        [Header("Shake")]
        [SerializeField] private CameraFollower _cameraFollower;
        [SerializeField] private CameraShaker _cameraShaker;

        [Header("Stun")]
        [SerializeField] private StunAndInvincibility _stunAndInvincibility;
        [SerializeField] private AnimationTransparency _animationTransparency;

        // Managers
        private GameLevelManager _gameLevelManager;
        private AdvertisingView _advertisingView;

        #endregion

        #region MONO

        private void Awake() => _gameLevelManager = GameLevelManager.Instance;

        private void OnEnable()
        {
            _health.Changed += TakeHit;
            _health.Dead += Dead;
        }

        private void OnDisable()
        {
            _health.Changed -= TakeHit;
            _health.Dead -= Dead;
        }

        #endregion

        #region TAKE HIT

        private void TakeHit()
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

        #endregion

        #region DEAD

        private async UniTask LoadAdvertisingViewAsync()
        {
            AdvertisingViewProvider provider = new();
            _advertisingView = await provider.LoadAsync(async () =>
            {
                _advertisingView.Finished -= TryRewardedRestartAsync;
                await UniTask.CompletedTask;
            });
            _advertisingView.Finished += TryRewardedRestartAsync;
        }

        private void TryRewardedRestartAsync(bool canGiveReward)
        {
            if (canGiveReward)
                Rebirth();
            else
                Restart();
        }

        private void Rebirth() => _health.Restart();
        private void Restart() => _gameLevelManager.Restart();

        #endregion

        #region CALLBACKS

        private async void Dead() => await LoadAdvertisingViewAsync();
        private void TakeHit(int heart) => TakeHit();

        #endregion
    }
}