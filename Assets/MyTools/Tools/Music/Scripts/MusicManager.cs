using UnityEngine;
using UnityEngine.Events;

namespace MyTools.Music
{
    public class MusicManager : MonoBehaviour
    {
        #region EVENTS

        public UnityAction<bool> MusicActiveChanged;
        public UnityAction<bool> SoundsActiveChanged;

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
                Destroy(gameObject);
        }

        #endregion

        #region INITIALIZE

        public void Initialize(bool isMusicActive, bool isSoundsActive)
        {
            IsMusicActive = isMusicActive;
            IsSoundsActive = isSoundsActive;
            
            UpdateMusicMuteState();
            UpdateSoundsMuteState();
        }

        #endregion

        #region UI

        public void PlayClickSound() => clickAudioSource.Play(); // only for those who created during the game

        private void SetDefaultVolume()
        {
            clickAudioSource.volume = defaultVolume;
            backgroundAudioSource.volume = defaultVolume;
        }

        private void UpdateMusicMuteState() => backgroundAudioSource.mute = !IsMusicActive;
        private void UpdateSoundsMuteState() => clickAudioSource.mute = !IsSoundsActive;

        #endregion

        #region VALUES

        public void SetIsActiveMusic(bool isMusicActive)
        {
            IsMusicActive = isMusicActive;
            UpdateMusicMuteState();
            InvokeIsMusicActiveChanged();
        }

        public void SetIsActiveSounds(bool isSoundsActive)
        {
            IsSoundsActive = isSoundsActive;
            UpdateSoundsMuteState();
            InvokeIsSoundsActiveChanged();
        }

        #endregion

        #region CALLBACKS

        private void InvokeIsMusicActiveChanged() => MusicActiveChanged?.Invoke(IsMusicActive);
        private void InvokeIsSoundsActiveChanged() => SoundsActiveChanged?.Invoke(IsSoundsActive);

        #endregion
    }
}