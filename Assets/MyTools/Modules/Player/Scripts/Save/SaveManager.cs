using MyTools.PlayerSystem.Save;

namespace MyTools.PlayerSystem
{
    public class SaveManager
    {
        private const bool USE_YG2 = true;

        private static ISaver strategy = USE_YG2 ? new YG2ISaver() : new PlayerPrefsISaver();

        public static int[] LoadStars() => strategy.LoadStars();
        public static void SaveStars(int[] stars) => strategy.SaveStars(stars);

        public static int LoadTrophy() => strategy.LoadTrophy();
        public static void SaveTrophies(int trophy) => strategy.SaveTrophy(trophy);

        public static int LoadMoney() => strategy.LoadMoney();
        public static void SaveMoney(int money) => strategy.SaveMoney(money);

        public static bool[] LoadActivitySkins() => strategy.LoadActivitySkins();
        public static void SaveActivitySkins(bool[] activitiesSkins) => strategy.SaveActivitySkins(activitiesSkins);

        public static int LoadNumberSelectedSkin() => strategy.LoadNumberSelectedSkin();
        public static void SaveNumberSelectedSkin(int numberSelectedSkin) => strategy.SaveNumberSelectedSkin(numberSelectedSkin);

        public static bool LoadMusicActive() => strategy.LoadMusicActive();
        public static void SaveMusicActive(bool active) => strategy.SaveMusicActive(active);

        public static bool LoadSoundsActive() => strategy.LoadSoundsActive();
        public static void SaveSoundsActive(bool active) => strategy.SaveSoundsActive(active);

        public static string LoadLanguage() => strategy.LoadLanguage();
        public static void SaveLanguage(string language) => strategy.SaveLanguage(language);
    }
}