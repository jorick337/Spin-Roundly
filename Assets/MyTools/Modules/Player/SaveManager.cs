using UnityEngine;

namespace MyTools.PlayerSystem
{
    public class SaveManager
    {
        #region LOAD

        public static int LoadMoney() => PlayerPrefs.GetInt("Money", 0);

        #endregion

        #region SAVE

        public static void SaveMoney(int money) => PlayerPrefs.SetInt("Money", money);

        #endregion
    }
}