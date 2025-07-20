using UnityEngine;

namespace MyTools.Levels.TwoDimensional.Objects.Health
{
    public class HL2_Sounds : MonoBehaviour
    {
        [SerializeField] private Health_HL2 _health;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _changedAudioClip;
        [SerializeField] private AudioClip _deadAudioClip;

        private void OnEnable()
        {
            _health.OnChanged += PlayChangedSound;
            _health.OnDead += PlayDeadSound;
        }

        private void OnDisable()
        {
            _health.OnChanged += PlayChangedSound;
            _health.OnDead += PlayDeadSound;
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

        private void PlayChangedSound(int health)
        {
            if (health != 3)
                PlayChangedSound();
        }
    }
}