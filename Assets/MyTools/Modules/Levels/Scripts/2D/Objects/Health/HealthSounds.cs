using UnityEngine;

namespace MyTools.Levels.TwoDimensional.Objects.Health
{
    public class HealthSounds : MonoBehaviour
    {
        [SerializeField] private Health _health;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _changedAudioClip;
        [SerializeField] private AudioClip _deadAudioClip;

        private void OnEnable()
        {
            _health.Changed += PlayChangedSound;
            _health.Dead += PlayDeadSound;
        }

        private void OnDisable()
        {
            _health.Changed += PlayChangedSound;
            _health.Dead += PlayDeadSound;
        }

        private void PlayChangedSound()
        {
            _audioSource.clip = _changedAudioClip;
            _audioSource.Play();
        }

        private void PlayDeadSound()
        {
            _audioSource.clip = _deadAudioClip;
            _audioSource.Play();
        }

        private void PlayChangedSound(int health) => PlayChangedSound();
    }
}