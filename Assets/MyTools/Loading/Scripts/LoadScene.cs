using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Load
{
    public class LoadScene : MonoBehaviour
    {
        public static LoadScene Instance { get; private set; }

        [Header("Core")]
        [SerializeField] private GameScenes gameScenes;

        public string Title { get; private set; }
        public bool IsAddressables = false;

        private void Awake()
        {
            Instance = this;

            if (gameScenes != null)
            {
                Title = gameScenes.GetTitleScene();
            }
        }

        public void Load()
        {
            SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings - 1);
            DontDestroyOnLoad(gameObject);
        }

        public void DestroySelf() => Destroy(gameObject);

        public void SetScene(int index) 
        {
            gameScenes.IndexScene = (Scenes)index;
            Title = gameScenes.GetTitleScene();
            Debug.Log($"{index} {gameScenes.GetTitleScene()}");
        }
    }
}