using System.Linq;
using UnityEngine;

namespace MyTools.PlayerSystem
{
    public class SaveManager
    {
        #region CONSTANTS

        private const int MAX_LEVEL = 15;
        private static readonly string LEVEL_STARS = string.Join(",", Enumerable.Repeat("0", MAX_LEVEL));

        private const int TROPHY = 0;
        private const int MONEY = 0;

        private const int MUSIC_ACTIVE = 1;
        private const int SOUNDS_ACTIVE = 1;

        private const string LANGUAGE = "ru";

        #endregion

        #region LOAD

        public static int[] LoadStars()
        {
            int[] levelStars = new int[MAX_LEVEL];

            string saved = PlayerPrefs.GetString("LevelStars", LEVEL_STARS);
            string[] parts = saved.Split(',');

            for (int i = 0; i < MAX_LEVEL; i++)
                levelStars[i] = int.Parse(parts[i]);

            return levelStars;
        }

        public static int LoadTrophy() => PlayerPrefs.GetInt("Trophy", TROPHY);
        public static int LoadMoney() => PlayerPrefs.GetInt("Money", MONEY);

        public static bool LoadMusisActive() => PlayerPrefs.GetInt("MusicActive", MUSIC_ACTIVE) == 1;
        public static bool LoadSoundsActive() => PlayerPrefs.GetInt("SoundsActive", SOUNDS_ACTIVE) == 1;

        public static string LoadLanguage() => PlayerPrefs.GetString("Language", LANGUAGE);

        #endregion

        #region SAVE

        public static void SaveStars(int[] levelStars) => PlayerPrefs.SetString("LevelStars", string.Join(",", levelStars));
        
        public static void SaveTrophies(int trophy) => PlayerPrefs.SetInt("Trophy", trophy);
        public static void SaveMoney(int money) => PlayerPrefs.SetInt("Money", money);

        public static void SaveMusicActive(bool active) => PlayerPrefs.SetInt("MusicActive", active ? 1 : 0);
        public static void SaveSoundsActive(bool active) => PlayerPrefs.SetInt("SoundsActive", active ? 1 : 0);

        public static void SaveLanguage(string local) => PlayerPrefs.SetString("Language", local);

        #endregion
    }
}