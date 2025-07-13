using System.Linq;
using UnityEngine;

namespace MyTools.PlayerSystem.Save
{
    public class PlayerPrefsISaver : ISaver
    {
        private const int MAX_LEVEL = 30;
        private static readonly string LEVEL_STARS = string.Join(",", Enumerable.Repeat("0", MAX_LEVEL));

        private const int MAX_SKINS = 9;
        private static readonly string SKINS = "True," + string.Join(",", Enumerable.Repeat("False", MAX_SKINS - 1));

        public int[] LoadStars()
        {
            string saved = PlayerPrefs.GetString("LevelStars", LEVEL_STARS);
            return saved.Split(',').Select(int.Parse).ToArray();
        }

        public void SaveStars(int[] stars) => PlayerPrefs.SetString("LevelStars", string.Join(",", stars));

        public int LoadTrophy() => PlayerPrefs.GetInt("Trophy", 0);
        public void SaveTrophy(int trophy) => PlayerPrefs.SetInt("Trophy", trophy);

        public int LoadMoney() => PlayerPrefs.GetInt("Money", 0);
        public void SaveMoney(int money) => PlayerPrefs.SetInt("Money", money);

        public bool[] LoadActivitySkins()
        {
            bool[] activitySkins = new bool[MAX_SKINS];
            string saved = PlayerPrefs.GetString("Skins", SKINS);
            string[] parts = saved.Split(',');

            for (int i = 0; i < MAX_SKINS; i++)
                activitySkins[i] = parts[i] == "True";

            return activitySkins;
        }

        public void SaveActivitySkins(bool[] activitySkins) => PlayerPrefs.SetString("Skins", string.Join(",", activitySkins));

        public int LoadNumberSelectedSkin() => PlayerPrefs.GetInt("NumberSelectedSkin", 1);
        public void SaveNumberSelectedSkin(int numberSelectedSkin) => PlayerPrefs.SetInt("NumberSelectedSkin", numberSelectedSkin);

        public bool LoadMusicActive() => PlayerPrefs.GetInt("MusicActive", 1) == 1;
        public void SaveMusicActive(bool active) => PlayerPrefs.SetInt("MusicActive", active ? 1 : 0);

        public bool LoadSoundsActive() => PlayerPrefs.GetInt("SoundsActive", 1) == 1;
        public void SaveSoundsActive(bool active) => PlayerPrefs.SetInt("SoundsActive", active ? 1 : 0);

        public string LoadLanguage() => PlayerPrefs.GetString("Language", "ru");
        public void SaveLanguage(string language) => PlayerPrefs.SetString("Language", language);
    }
}