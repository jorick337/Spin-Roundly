namespace YG
{
    public partial class SavesYG
    {
        public int[] Stars = new int[30];
        public int Trophy = 0;
        public int Money = 0;

        public bool[] ActivitySkins = new bool[9] { true, false, false, false, false, false, false, false, false };
        public int NumberSelectedSkin = 1;

        public bool MusicActive = true;
        public bool SoundsActive = true;

        public string Language = YG2.envir.language;
    }
}