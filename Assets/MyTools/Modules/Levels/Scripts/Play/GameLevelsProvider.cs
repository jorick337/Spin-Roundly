using MyTools.LocalAddressables;

namespace MyTools.Levels.Play
{
    public class GameLevelsProvider : LocalAssetLoader
    {
        public async void Load(int level)
        {
            GameLevel gameLevel = await LoadInternal<GameLevel>($"Level {level}");
            gameLevel.SetGameLevelsProvider(this);
        }
    }
}