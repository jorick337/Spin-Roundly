using Game.Localization;
using MyTools.Music;
using UnityEngine;

namespace MyTools.PlayerSystem
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager Instance { get; private set; }

        public Player Player { get; private set; }

        [Header("Managers")]
        [SerializeField] private LanguageManager _languageManager;
        [SerializeField] private MusicManager _musicManager;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);

                Player = new();
            }
            else
                Destroy(gameObject);
        }

        private void Start()
        {
            InitializeLanguage();
            InitializeMusic();
        }

        private void OnEnable()
        {
            _languageManager.LocaleChanged += SaveLanguage;
            _musicManager.MusicActiveChanged += SaveMusicActive;
            _musicManager.SoundsActiveChanged += SaveSoundsActive;
        }

        private void OnDisable()
        {
            _languageManager.LocaleChanged -= SaveLanguage;
            _musicManager.MusicActiveChanged -= SaveMusicActive;
            _musicManager.SoundsActiveChanged -= SaveSoundsActive;
        }

        private async void InitializeLanguage() => await _languageManager.InitializeAsync(SaveManager.LoadLanguage());
        private void InitializeMusic() => _musicManager.Initialize(SaveManager.LoadMusisActive(), SaveManager.LoadSoundsActive());

        private void SaveLanguage(string locale) => SaveManager.SaveLanguage(locale);
        private void SaveMusicActive(bool active) => SaveManager.SaveMusicActive(active);
        private void SaveSoundsActive(bool active) => SaveManager.SaveSoundsActive(active);
    }
}