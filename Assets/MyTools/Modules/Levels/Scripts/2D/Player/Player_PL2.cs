using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using MyTools.Levels.Play;
using MyTools.Levels.TwoDimensional.Objects.Health;
using MyTools.Movement.TwoDimensional;
using MyTools.UI;
using MyTools.UI.Objects;
using UnityEngine;

namespace MyTools.Levels.TwoDimensional.Player
{
    public class Player_PL2 : MonoBehaviour
    {
        public Func<UniTask> OnDead;

        [SerializeField] private Movement2D _movement2D;
        [SerializeField] private Health_HL2 _health;
        [SerializeField] private Teleport _teleportPlayer;
        [SerializeField] private CollusionIgnore _stunAndInvincibility;
        [SerializeField] private float _timeToRebirth;

        // Managers
        private GameLevelManager _gameLevelManager;

        private void Awake() => _gameLevelManager = GameLevelManager.Instance;

        private void OnEnable()
        {
            _gameLevelManager.OnFinish += Finish;
            _gameLevelManager.OnPreRestart += RestartAsync;
            _gameLevelManager.OnRestart += Restart;
            _health.OnDead += Dead;
        }

        private void OnDisable()
        {
            _gameLevelManager.OnFinish -= Finish;
            _gameLevelManager.OnPreRestart -= RestartAsync;
            _gameLevelManager.OnRestart -= Restart;
            _health.OnDead -= Dead;
        }

        #region CORE LOGIC

        public async UniTask RestartAsync() => await Handle(async () => await WaitTimeToRebirth());

        public void Restart() 
        {
            _health.Restart();
            _teleportPlayer.SendToTarget();
            Enable();
        }

        public async void Rebirth() => await Handle(async () =>
        {
            _health.Restart();
            await UniTask.CompletedTask;
        });

        private void Finish() => Disable();
        private async void Dead() => await Handle(async () => await InvokeOnDead());

        #endregion

        private async UniTask Handle(Func<UniTask> action)
        {
            Disable();
            if (action != null)
                await action.Invoke();
            Enable();
        }

        private void Disable()
        {
            _movement2D.Disable();
            _stunAndInvincibility.DisableCollision();
        }

        private async void Enable()
        {
            _movement2D.Enable();
            await WaitTimeToRebirth();
            _stunAndInvincibility.EnableCollision();
        }

        private async UniTask WaitTimeToRebirth() => await UniTask.WaitForSeconds(_timeToRebirth);

        #region CALLBACKS

        private async UniTask InvokeOnDead()
        {
            foreach (Func<UniTask> handler in OnDead.GetInvocationList().Cast<Func<UniTask>>())
                await handler();
        }

        #endregion
    }
}