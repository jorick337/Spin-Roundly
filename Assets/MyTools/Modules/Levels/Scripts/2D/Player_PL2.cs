using Cysharp.Threading.Tasks;
using MyTools.Levels.TwoDimensional.Objects.Health;
using MyTools.Movement.TwoDimensional;
using MyTools.UI;
using MyTools.UI.Objects;
using UnityEngine;

namespace MyTools.Levels.TwoDimensional.Player
{
    public class Player_PL2 : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private Movement2D _movement2D;
        [SerializeField] private Health_HL2 _health;
        [SerializeField] private Teleport _teleportPlayer;
        [SerializeField] private CollusionIgnore _stunAndInvincibility;
        [SerializeField] private float _timeToRestart;

        public void Disable()
        {
            _movement2D.Disable();
            _stunAndInvincibility.DisableCollision();
        }

        public async UniTask Enable()
        {
            _movement2D.Enable();
            await UniTask.WaitForSeconds(_timeToRestart);
            _stunAndInvincibility.EnableCollision();
        }

        private void SendToTarget()
        {
            _rigidbody2D.simulated = false;
            _teleportPlayer.SendToTarget();
            ResetSpeed();
            _rigidbody2D.simulated = true;
        }

        private void ResetSpeed()
        {
            _rigidbody2D.linearVelocity = Vector2.zero;
            _rigidbody2D.linearVelocityY = 0f;
            _rigidbody2D.angularVelocity = 0f;
        }

        public async UniTask Rebirth()
        {
            Disable();
            _health.Restart();
            await Enable();
        }

        public async UniTask Restart()
        {
            Disable();
            _health.Restart();
            SendToTarget();
            await Enable();
        }
    }
}