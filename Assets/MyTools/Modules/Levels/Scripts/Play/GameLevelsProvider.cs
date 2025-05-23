using System;
using Cysharp.Threading.Tasks;
using MyTools.LocalAddressables;

namespace MyTools.Levels.Play
{
    public class GameLevelsProvider : LocalAssetLoader
    {
        public async UniTask<GameLevel> Load(int level, Func<UniTask> action)
        {
            GameLevel gameLevel = await LoadInternal<GameLevel>($"Level {level}");
            gameLevel.SetGameLevelsProvider(this);
            AddEvent(action);

            return gameLevel;
        }
    }
}