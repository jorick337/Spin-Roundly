using System.Threading.Tasks;
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

        [Header("Finish")]
        [SerializeField] private Collider2DTrigger _finishColliderTrigger;
        [SerializeField] private Movement2D _movement2D;

        [Header("Restart")]
        [SerializeField] private Collider2DTrigger _defeatColliderTrigger;
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
            _finishColliderTrigger.OnTriggered += FinishLevelByCollider;
            _defeatColliderTrigger.OnTriggered += RestartLevelByCollider;
            _playerHealth.Changed += TakeHit;
        }

        private void OnDisable()
        {
            _gameLevelManager.OnFinish -= Finish;
            _gameLevelManager.OnRestart -= Restart;
            _finishColliderTrigger.OnTriggered -= FinishLevelByCollider;
            _defeatColliderTrigger.OnTriggered -= RestartLevelByCollider;
            _playerHealth.Changed -= TakeHit;
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

        private void FinishLevelByCollider(Collider2D collider2D) => FinishLevel();
        private void RestartLevelByCollider(Collider2D collider2D) => RestartLevel();

        private void FinishLevel() => _gameLevelManager.Finish();
        private void RestartLevel() => _gameLevelManager.Restart();

        private void TakeHit(int heart) => StunAndAnimatePlayerAsync();

        #endregion
    }
}