using System;
using Cysharp.Threading.Tasks;
using MyTools.LocalAddressables;

namespace MyTools.Levels.Play
{
    public class GameLevelsProvider : LocalAssetLoader
    {
        public async UniTask<GameLevel> Load(int level)
        {
            GameLevel gameLevel = await LoadGameObjectAsync<GameLevel>($"Level {level}");
            gameLevel.SetProvider(this);

            return gameLevel;
        }
    }
}