using System.Linq;
using UnityEngine;

namespace MyTools.Shop.Skins
{
    public static class SSHV_Saver
    {
        #region CONSTANTS

        private const int MAX_SKINS = 9;
        private static readonly string SKINS = "True," + string.Join(",", Enumerable.Repeat("False", MAX_SKINS - 1));

        private const int NUMBER = 1;

        #endregion

        #region LOAD

        public static bool[] LoadSkins()
        {
            bool[] activitySkins = new bool[MAX_SKINS];
            string saved = PlayerPrefs.GetString("Skins", SKINS);
            string[] parts = saved.Split(',');

            for (int i = 0; i < MAX_SKINS; i++)
                activitySkins[i] = parts[i] == "True";

            return activitySkins;
        }

        public static int LoadNumberSkin() => PlayerPrefs.GetInt("NumberSkin", NUMBER);

        #endregion

        #region SAVE

        public static void SaveSkins(bool[] activities) => PlayerPrefs.SetString("Skins", string.Join(",", activities));
        public static void SaveNumber(int number) => PlayerPrefs.SetInt("NumberSkin", number);

        #endregion
    }
}