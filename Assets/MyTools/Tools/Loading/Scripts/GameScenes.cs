using UnityEngine;

namespace Game.Load
{
    public enum Scenes
    {
        Start = 0,
        Level1 = 1,
        Level2 = 2,
        Level3 = 3,
        Level4 = 4,
        Level5 = 5,
    }

    public class GameScenes : MonoBehaviour
    {
        public Scenes IndexScene;

        public string GetTitleScene() => IndexScene.ToString();
    }
}