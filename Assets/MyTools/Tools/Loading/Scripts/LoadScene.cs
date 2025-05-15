using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyTools.Loading
{
    public class LoadScene : MonoBehaviour
    {
        public static LoadScene Instance { get; private set; }

        [SerializeField] private GameScenes _gameScenes;

        public string Title { get; private set; }
        public bool IsAddressables = false;

        private void Awake()
        {
            Instance = this;

            if (_gameScenes != null)
            {
                Title = _gameScenes.GetTitleScene();
            }
        }

        public void Load()
        {
            DontDestroyOnLoad(gameObject);
            SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings - 1);
        }

        public async UniTask LoadAsync()
        {
            DontDestroyOnLoad(gameObject);
            await SceneManager.LoadSceneAsync(SceneManager.sceneCountInBuildSettings - 1);
        }

        public void DestroySelf() => Destroy(gameObject);

        public void SetScene(int index) 
        {
            _gameScenes.IndexScene = (Scenes)index;
            Title = _gameScenes.GetTitleScene();
        }
    }
}