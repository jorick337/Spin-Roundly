using MyTools.LocalAddressables;

namespace MyTools.Levels.Play
{
    public class VictoryViewProvider : LocalAssetLoader
    {
        public async void Load(int level)
        {
            VictoryView victoryView = await LoadInternal<VictoryView>("VictoryView");
            victoryView.SetProvider(this);
        }
    }
}