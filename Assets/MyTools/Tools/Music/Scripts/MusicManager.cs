using UnityEngine;
using UnityEngine.Audio;
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
        [SerializeField] private float _defaultVolume;
        [SerializeField] private AudioMixer _mixer;

        [Header("UI")]
        [SerializeField] private AudioSource _clickAudioSource;
        [SerializeField] private AudioSource _backgroundAudioSource;

        public bool IsMusicActive { get; private set; } = true;
        public bool IsSoundsActive { get; private set; } = true;

        public bool IsLoaded { get; private set; } = false;

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

            IsLoaded = true;
        }

        #endregion

        #region UI

        public void PlayClickSound() => _clickAudioSource.Play(); // only for those who created during the game

        private void SetDefaultVolume() => _mixer.SetFloat("Volume", Mathf.Lerp(-80, 0, _defaultVolume));

        private void UpdateMusicMuteState() => _backgroundAudioSource.mute = !IsMusicActive;
        private void UpdateSoundsMuteState() => _clickAudioSource.mute = !IsSoundsActive;

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