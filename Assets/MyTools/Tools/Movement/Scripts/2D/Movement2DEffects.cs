using UnityEngine;

namespace MyTools.Movement.TwoDimensional
{
    public class Movement2DEffects : MonoBehaviour
    {
        [Header("Core")]
        [SerializeField] private AudioSource _audioSource;

        [Header("Jump")]
        [SerializeField] private AudioClip _jumpSound;
        [SerializeField] private ParticleSystem _jumpParticleSystem;

        private Movement2D _movement2D;

        private void Awake() => _movement2D = Movement2D.Instance;
        private void OnEnable() => _movement2D.OnJump += PlayJumpEffects;
        private void OnDisable() => _movement2D.OnJump -= PlayJumpEffects;

        private void PlayJumpEffects()
        {
            _audioSource.clip = _jumpSound;
            _jumpParticleSystem.Play();
            _audioSource.Play();
        }
    }
}