using UnityEngine;

namespace MyTools.PlayerSystem
{
    public class SaveManager
    {
        #region LOAD

        public static int LoadMoney() => PlayerPrefs.GetInt("Money", 0);

        public static bool LoadMusisActive() => PlayerPrefs.GetInt("MusicActive", 1) == 1;
        public static bool LoadSoundsActive() => PlayerPrefs.GetInt("SoundsActive", 1) == 1;

        public static string LoadLanguage() => PlayerPrefs.GetString("Language", "ru");

        #endregion

        #region SAVE

        public static void SaveMoney(int money) => PlayerPrefs.SetInt("Money", money);

        public static void SaveMusicActive(bool active) => PlayerPrefs.SetInt("MusicActive", active ? 1 : 0);
        public static void SaveSoundsActive(bool active) => PlayerPrefs.SetInt("SoundsActive", active ? 1 : 0);

        public static void SaveLanguage(string local) => PlayerPrefs.SetString("Language", local);

        #endregion
    }
}