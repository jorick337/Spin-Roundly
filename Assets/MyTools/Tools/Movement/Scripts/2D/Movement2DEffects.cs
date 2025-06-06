using UnityEngine;

namespace MyTools.Movement.TwoDimensional
{
    public class Movement2DSounds : MonoBehaviour
    {
        [Header("Core")]
        [SerializeField] private Movement2D _movement2D;
        [SerializeField] private AudioSource _audioSource;

        [Header("Jump")]
        [SerializeField] private ParticleSystem _jumpParticleSystem;
        [SerializeField] private AudioClip _jumpSound;

        private void OnEnable() => _movement2D.OnJump += PlayJumpSound;
        private void OnDisable() => _movement2D.OnJump -= PlayJumpSound;

        private void PlayJumpSound() 
        {
            _audioSource.clip = _jumpSound;
            _jumpParticleSystem.Play();
            _audioSource.Play();
        }
    }
}