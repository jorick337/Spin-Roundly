using UnityEngine;

namespace MyTools.UI.Colliders
{
    public class EffectColliderTrigger : MonoBehaviour
    {
        [SerializeField] private ColliderTrigger2D _colliderTrigger2D;
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private AudioSource _audioSource;

        private void OnEnable()
        {
            _colliderTrigger2D.OnTriggeredEnter += Enter;
            _colliderTrigger2D.OnTriggeredExit += Exit;
        }

        private void OnDisable()
        {
            _colliderTrigger2D.OnTriggeredEnter -= Enter;
            _colliderTrigger2D.OnTriggeredExit -= Exit;
        }

        private void OnValidate()
        {
            if (_colliderTrigger2D == null)
                _colliderTrigger2D = GetComponent<ColliderTrigger2D>();
            if (_audioSource == null)
                _audioSource = GetComponent<AudioSource>();
            if (_particleSystem == null)
                _particleSystem = GetComponent<ParticleSystem>();
        }

        private void Enter(Collider2D collider)
        {
            if (_particleSystem != null)
                _particleSystem.Play();

            if (_audioSource != null)
                _audioSource.Play();
        }

        private void Exit(Collider2D collider)
        {
            if (_particleSystem != null)
                _particleSystem.Stop();

            if (_audioSource != null)
                _audioSource.Stop();
        }
    }
}