using MyTools.UI;
using UnityEngine;
using UnityEngine.Events;

namespace MyTools.Levels.Play
{
    public class Health : MonoBehaviour
    {
        public event UnityAction<int> Changed;
        public event UnityAction Dead;

        [Header("Core")]
        [SerializeField] private Collider2DTrigger _collider2DTrigger;
        [SerializeField] private int _default = 3;
        [SerializeField] private bool _instance;

        [Header("Sounds")]
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _hitAudioClip;
        [SerializeField] private AudioClip _deadAudioClip;

        public static Health Instance { get; private set; }
        public int Current { get; private set; }

        private void Awake() => Initialize();
        private void OnEnable() => _collider2DTrigger.OnTriggered += Add;

        private void OnDisable() => _collider2DTrigger.OnTriggered -= Add; 

        private void Initialize()
        {
            if (_instance)
                Instance = this;

            Current = _default;
        }

        public void Restart()
        {
            Current = _default;
            InvokeChanged();
        }

        private void Add(Collider2D collider2D)
        {
            Current -= 1;
            
            PlayHitSound();
            InvokeChanged();

            if (Current == 0)
            {
                PlayDeadSound();
                InvokeDead();
            }
        }

        private void PlayHitSound() 
        {
            _audioSource.clip = _hitAudioClip;
            _audioSource.Play();
        }

        private void PlayDeadSound() 
        {
            _audioSource.clip = _deadAudioClip;
            _audioSource.Play();
        } 

        private void InvokeChanged() => Changed?.Invoke(Current);
        private void InvokeDead() => Dead?.Invoke();
    }
}