using UnityEngine;

namespace MyTools.UI.Colliders
{
    public class EffectColliderTrigger : TriggerActivator
    {
        [Header("Effects")]
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private AudioSource _audioSource;

        protected override void OnValidate()
        {
            base.OnValidate();

            if (_audioSource == null)
                _audioSource = GetComponent<AudioSource>();

            if (_particleSystem == null)
                _particleSystem = GetComponent<ParticleSystem>();
        }

        protected override void Enter(Collider2D collider)
        {
            if (_particleSystem != null)
                _particleSystem.Play();

            if (_audioSource != null)
                _audioSource.Play();
        }

        protected override void Exit(Collider2D collider)
        {
            if (_particleSystem != null)
                _particleSystem.Stop();

            if (_audioSource != null)
                _audioSource.Stop();
        }
    }
}