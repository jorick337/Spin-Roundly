using Cysharp.Threading.Tasks;
using MyTools.Movement.TwoDimensional;
using MyTools.UI;
using MyTools.UI.Animation;
using MyTools.UI.Objects;
using UnityEngine;

namespace MyTools.Levels.Play
{
    public class GameLevel : MonoBehaviour
    {
        #region  CORE

        [Header("Core")]
        [SerializeField] private Movement2D _movement2D;
        [SerializeField] private Teleport _teleportPlayer;

        [Header("Player")]
        [SerializeField] private StunAndInvincibility _playerStunAndInvincibility;
        [SerializeField] private Health _playerHealth;
        [SerializeField] private AnimationTransparency _playerAnimationTransparency;

        // Managers
        private GameLevelManager _gameLevelManager;
        private GameLevelsProvider _gameLevelsProvider;

        #endregion

        #region MONO

        private void Awake() => _gameLevelManager = GameLevelManager.Instance;

        private void OnEnable()
        {
            _gameLevelManager.OnFinish += Finish;
            _gameLevelManager.OnRestart += Restart;
            _playerHealth.Changed += StunAndAnimatePlayerAsync;
        }

        private void OnDisable()
        {
            _gameLevelManager.OnFinish -= Finish;
            _gameLevelManager.OnRestart -= Restart;
            _playerHealth.Changed -= StunAndAnimatePlayerAsync;
        }

        #endregion

        #region CORE LOGIC

        private async void StunAndAnimatePlayerAsync()
        {
            _playerStunAndInvincibility.Stun();
            _playerAnimationTransparency.StartAlwaysAnimation();
            await _playerStunAndInvincibility.WaitUntilFinished();
            _playerAnimationTransparency.StopAlwaysAnimation();
        }

        #endregion

        #region VALUES

        public void SetProvider(GameLevelsProvider gameLevelsProvider) => _gameLevelsProvider = gameLevelsProvider;
        public UniTask Unload() => _gameLevelsProvider.UnloadAsync();

        #endregion

        #region CALLBACKS

        private void Restart()
        {
            StunAndAnimatePlayerAsync();
            _teleportPlayer.SendToTarget();
            _movement2D.Enable();
        }

        private void Finish() => _movement2D.Disable();

        private void StunAndAnimatePlayerAsync(int heart) => StunAndAnimatePlayerAsync();

        #endregion
    }
}