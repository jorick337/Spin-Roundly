using MyTools.UI;
using UnityEngine;

namespace MyTools.Levels.TwoDimensional.Player
{
    public class PL2_EffectTrigger : MonoBehaviour
    {
        [SerializeField] private Collider2DTrigger _collider2DTrigger;
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private AudioSource _audioSource;

        private void OnEnable()
        {
            _collider2DTrigger.OnTriggeredEnter += Enter;
            _collider2DTrigger.OnTriggeredExit += Exit;
        }

        private void OnDisable()
        {
            _collider2DTrigger.OnTriggeredEnter -= Enter;
            _collider2DTrigger.OnTriggeredExit -= Exit;
        }

        private void Enter(Collider2D collider2D)
        {
            _particleSystem.Play();
            _audioSource.Play();
        }

        private void Exit(Collider2D collider2D)
        {
            _particleSystem.Stop();
            _audioSource.Stop();
        }
    }
}