// using Game.Localization;
using MyTools.Levels;
using MyTools.Music;
using UnityEngine;
using UnityEngine.Events;
using YG;

namespace MyTools.PlayerSystem
{
    public class PlayerManager : MonoBehaviour
    {
        public event UnityAction OnLoaded;

        public static PlayerManager Instance { get; private set; }

        public Player Player { get; private set; }

        [Header("Managers")]
        [SerializeField] private LevelsManager _levelsManager;
        // [SerializeField] private LanguageManager _languageManager;
        [SerializeField] private MusicManager _musicManager;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                Player = new();
                DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(gameObject);
        }

        private void OnEnable()
        {
            YG2.onGetSDKData += Initialize;

            _levelsManager.TrophiesChanged += SaveManager.SaveTrophies;
            _levelsManager.StarsChanged += SaveManager.SaveStars;
            // _languageManager.LocaleChanged += SaveManager.SaveLanguage;
            _musicManager.MusicActiveChanged += SaveManager.SaveMusicActive;
            _musicManager.SoundsActiveChanged += SaveManager.SaveSoundsActive;
        }

        private void OnDisable()
        {
            YG2.onGetSDKData -= Initialize;

            _levelsManager.TrophiesChanged -= SaveManager.SaveTrophies;
            _levelsManager.StarsChanged -= SaveManager.SaveStars;
            // _languageManager.LocaleChanged -= SaveManager.SaveLanguage;
            _musicManager.MusicActiveChanged -= SaveManager.SaveMusicActive;
            _musicManager.SoundsActiveChanged -= SaveManager.SaveSoundsActive;
        }

        private void Initialize()
        {
            if (YG2.isSDKEnabled)
            {
                Player.Initialize();
                InitializeLevels();
                // InitializeLanguage();
                InitializeMusic();
                InvokeOnLoaded();
            }
        }

        private void InitializeLevels() => _levelsManager.Initialize(SaveManager.LoadStars(), SaveManager.LoadTrophy());
        // private void InitializeLanguage() => _languageManager.Initialize(SaveManager.LoadLanguage());
        private void InitializeMusic() => _musicManager.Initialize(SaveManager.LoadMusicActive(), SaveManager.LoadSoundsActive());

        private void InvokeOnLoaded() => OnLoaded?.Invoke();
    }
}