using Cysharp.Threading.Tasks;
using UnityEngine;

namespace MyTools.Levels.Play
{
    public class GameLevel : MonoBehaviour
    {
        // Managers
        private GameLevelsProvider _provider;

        public void SetProvider(GameLevelsProvider provider) => _provider = provider;
        public UniTask Unload() => _provider.UnloadAsync();
    }
}