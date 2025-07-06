using Cysharp.Threading.Tasks;
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
        [SerializeField] private float _generalVolume;
        [SerializeField] private AudioMixer _mixer;

        public bool IsMusicActive { get; private set; } = true;
        public bool IsSoundsActive { get; private set; } = true;

        private bool _isLoaded = false;

        #endregion

        #region MONO

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(gameObject);
        }

        private void Start()
        {
            if (Instance != null)
                UpdateGeneralVolume();
        }

        #endregion

        #region INITIALIZE

        public void Initialize(bool isMusicActive, bool isSoundsActive)
        {
            IsMusicActive = isMusicActive;
            IsSoundsActive = isSoundsActive;

            UpdateBackgroundVolume();
            UpdateSoundsVolume();

            _isLoaded = true;
        }

        public async UniTask WaitUntilLoaded() => await UniTask.WaitUntil(() => _isLoaded == true);

        #endregion

        #region UI

        private void UpdateGeneralVolume() => _mixer.SetFloat("GeneralVolume", Mathf.Lerp(-80, 0, _generalVolume));
        private void UpdateBackgroundVolume() => _mixer.SetFloat("MusicVolume", Mathf.Lerp(-80, 0, IsMusicActive ? 1 : 0));
        private void UpdateSoundsVolume() => _mixer.SetFloat("SoundsVolume", Mathf.Lerp(-80, 0, IsMusicActive ? 1 : 0));

        #endregion

        #region VALUES

        public void SetIsActiveMusic(bool isMusicActive)
        {
            IsMusicActive = isMusicActive;
            UpdateBackgroundVolume();
            InvokeIsMusicActiveChanged();
        }

        public void SetIsActiveSounds(bool isSoundsActive)
        {
            IsSoundsActive = isSoundsActive;
            UpdateSoundsVolume();
            InvokeIsSoundsActiveChanged();
        }

        #endregion

        #region CALLBACKS

        private void InvokeIsMusicActiveChanged() => MusicActiveChanged?.Invoke(IsMusicActive);
        private void InvokeIsSoundsActiveChanged() => SoundsActiveChanged?.Invoke(IsSoundsActive);

        #endregion
    }
}