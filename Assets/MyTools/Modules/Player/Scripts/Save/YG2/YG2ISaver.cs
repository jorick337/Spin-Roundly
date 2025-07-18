using YG;

namespace MyTools.PlayerSystem.Save
{
    public class YG2ISaver : ISaver
    {
        public int[] LoadStars() => YG2.saves.Stars;
        public void SaveStars(int[] stars) 
        {
            YG2.saves.Stars = stars;
            YG2.SaveProgress();
        }

        public int LoadTrophy() => YG2.saves.Trophy;
        public void SaveTrophy(int trophy) 
        {
            YG2.saves.Trophy = trophy;
            YG2.SetLeaderboard("Leaderboard", trophy);
            YG2.SaveProgress();
        }

        public int LoadMoney() => YG2.saves.Money;
        public void SaveMoney(int money) 
        {
            YG2.saves.Money = money;
            YG2.SaveProgress();
        } 

        public bool[] LoadActivitySkins() => YG2.saves.ActivitySkins;
        public void SaveActivitySkins(bool[] activitySkins) 
        {
            YG2.saves.ActivitySkins = activitySkins;
            YG2.SaveProgress();
        } 

        public int LoadNumberSelectedSkin() => YG2.saves.NumberSelectedSkin;
        public void SaveNumberSelectedSkin(int numberSelectedSkin) 
        {
            YG2.saves.NumberSelectedSkin = numberSelectedSkin;
            YG2.SaveProgress();
        } 

        public bool LoadMusicActive() => YG2.saves.MusicActive;
        public void SaveMusicActive(bool active) 
        {
            YG2.saves.MusicActive = active;
            YG2.SaveProgress();
        } 

        public bool LoadSoundsActive() => YG2.saves.SoundsActive;
        public void SaveSoundsActive(bool active) 
        {
            YG2.saves.SoundsActive = active;
            YG2.SaveProgress();
        } 

        public string LoadLanguage() => YG2.saves.Language;
        public void SaveLanguage(string language) 
        {
            YG2.saves.Language = language;
            YG2.SaveProgress();
        }
    }
}