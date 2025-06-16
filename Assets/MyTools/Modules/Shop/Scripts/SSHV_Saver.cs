using System.Linq;
using UnityEngine;

namespace MyTools.Shop.Skins
{
    public class SSHV_Saver
    {
        private const int MAX_SKINS = 9;
        private static readonly string SKINS = string.Join(",", Enumerable.Repeat("0", MAX_SKINS));

        private const int NUMBER = 1;

        #region LOAD

        public static bool[] LoadSkins()
        {
            bool[] activitySkins = new bool[MAX_SKINS];
            
            string saved = PlayerPrefs.GetString("Skins", SKINS);
            string[] parts = saved.Split(',');

            for (int i = 0; i < MAX_SKINS; i++)
                activitySkins[i] = parts[i] != "0";

            return activitySkins;
        }

        public static int LoadNumberSkin() => PlayerPrefs.GetInt("NumberSkin", NUMBER);

        #endregion

        #region SAVE

        public static void SaveSkins(bool[] activities) => PlayerPrefs.SetString("ActivitySkins", string.Join(",", activities));
        public static void SaveNumber(int number) => PlayerPrefs.SetInt("NumberSkin", number);

        #endregion
    }
}