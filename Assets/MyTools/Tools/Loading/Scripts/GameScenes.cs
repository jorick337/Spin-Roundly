using UnityEngine;

namespace MyTools.Loading
{
    public enum Scenes
    {
        Start = 0,
        Levels = 1
    }

    public class GameScenes : MonoBehaviour
    {
        public Scenes IndexScene;

        public string GetTitleScene() => IndexScene.ToString();
    }
}