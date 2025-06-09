using MyTools.Levels.Play;
using MyTools.Levels.TwoDimensional.Objects.Health;
using MyTools.Movement.TwoDimensional;
using MyTools.UI;
using UnityEngine;

namespace MyTools.Levels.TwoDimensional.Player
{
    public class Player_PL2 : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private Movement2D _movement2D;
        [SerializeField] private Health_HL2 _health;
        [SerializeField] private Teleport _teleportPlayer;

        [SerializeField] private float _inactiveMass;
        [SerializeField] private float _activeMass;

        public void Disable()
        {
            _health.enabled = false;
            _rigidbody2D.mass = _inactiveMass;
            _movement2D.Disable();
        }

        public void Enable()
        {
            _health.enabled = true;
            _rigidbody2D.mass = _activeMass;
            _movement2D.Enable();
        }

        public void Restart()
        {
            Enable();
            _teleportPlayer.SendToTarget();
            _health.Restart();
        }
    }
}