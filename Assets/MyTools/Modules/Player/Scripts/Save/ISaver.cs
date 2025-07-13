namespace MyTools.PlayerSystem.Save
{
    public interface ISaver
    {
        void SaveStars(int[] stars);
        int[] LoadStars();

        void SaveTrophy(int trophy);
        int LoadTrophy();

        void SaveMoney(int money);
        int LoadMoney();

        bool[] LoadActivitySkins();
        void SaveActivitySkins(bool[] activities);

        int LoadNumberSelectedSkin();
        void SaveNumberSelectedSkin(int numberSelectedSkin);

        void SaveMusicActive(bool active);
        bool LoadMusicActive();

        void SaveSoundsActive(bool active);
        bool LoadSoundsActive();

        void SaveLanguage(string local);
        string LoadLanguage();
    }
}