using UnityEngine;

namespace MyTools.UI.Colliders
{
    public class EffectColliderTrigger : MonoBehaviour
    {
        [SerializeField] private ColliderTrigger2D _colliderTrigger2D;
        [SerializeField] private AudioSource _audioSource;

        private void OnEnable() => _colliderTrigger2D.OnTriggeredEnter += PlayAudioSource;
        private void OnDisable() => _colliderTrigger2D.OnTriggeredEnter -= PlayAudioSource;

        private void OnValidate()
        {
            if (_colliderTrigger2D == null)
                _colliderTrigger2D = GetComponent<ColliderTrigger2D>();
            if (_audioSource == null)
                _audioSource = GetComponent<AudioSource>();
        }

        private void PlayAudioSource(Collider2D collider2D) => _audioSource?.Play();
    }
}