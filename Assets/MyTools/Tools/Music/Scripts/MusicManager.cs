using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Music
{
    public class MusicManager : MonoBehaviour
    {
        #region EVENTS

        public Action IsMusicActiveChanged;
        public Action IsSoundsActiveChanged;

        #endregion

        #region CORE

        public static MusicManager Instance { get; private set; }

        [Header("Core")]
        [SerializeField] private float defaultVolume;

        [Header("UI")]
        [SerializeField] private AudioSource clickAudioSource;
        [SerializeField] private AudioSource backgroundAudioSource;

        public bool IsMusicActive { get; private set; } = true;
        public bool IsSoundsActive { get; private set; } = true;

        #endregion

        #region MONO

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                
                SetDefaultVolume();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        #endregion

        #region INITIALIZE

        public void Initialize(bool isMusicActive, bool isSoundsActive)
        {
            IsMusicActive = isMusicActive;
            IsSoundsActive = isSoundsActive;
            
            UpdateActiveMusic();
            UpdateActiveSounds();
        }

        #endregion

        #region UI

        public void PlayClickSound() => clickAudioSource.Play(); // only for those who created during the game

        private void SetDefaultVolume()
        {
            clickAudioSource.volume = defaultVolume;
            backgroundAudioSource.volume = defaultVolume;
        }

        private void UpdateActiveMusic() => backgroundAudioSource.mute = !IsMusicActive;
        private void UpdateActiveSounds() => clickAudioSource.mute = !IsSoundsActive;

        #endregion

        #region CALLBACKS

        public void SwitchActiveMusic()
        {
            IsMusicActive = !IsMusicActive;
            UpdateActiveMusic();
            IsMusicActiveChanged?.Invoke();
        }

        public void SwitchActiveSounds()
        {
            IsSoundsActive = !IsSoundsActive;
            UpdateActiveSounds();
            IsSoundsActiveChanged?.Invoke();
        }

        #endregion
    }
}