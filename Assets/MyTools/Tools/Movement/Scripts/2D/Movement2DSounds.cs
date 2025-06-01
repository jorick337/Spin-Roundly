using UnityEngine;

namespace MyTools.Movement.TwoDimensional
{
    public class Movement2DSounds : MonoBehaviour
    {
        [Header("Core")]
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _jumpSound;

        [Header("Managers")]
        [SerializeField] private Movement2D _movement2D;

        private void OnEnable() => _movement2D.OnJump += PlayJumpSound;
        private void OnDisable() => _movement2D.OnJump -= PlayJumpSound;

        private void PlayJumpSound() 
        {
            _audioSource.clip = _jumpSound;
            _audioSource.Play();
        }
    }
}