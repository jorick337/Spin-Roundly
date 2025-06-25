using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyTools.Loading
{
    public class LoadScene : MonoBehaviour
    {
        public static LoadScene Instance { get; private set; }

        [SerializeField] private GameScenes _gameScenes;
        [SerializeField] private bool _useLoadingScene = false;

        public string Title { get; private set; }
        public bool IsAddressables = false;

        private void Awake()
        {
            Instance = this;

            if (_gameScenes != null)
                Title = _gameScenes.GetTitleScene();
        }

        #region ASYNC

        public async UniTask LoadAsync()
        {
            if (_useLoadingScene)
                await LoadWithLoaderSceneAsync();
            else
                await LoadWithoutLoaderSceneAsync();
        }
        
        private async UniTask LoadWithLoaderSceneAsync()
        {
            DontDestroyOnLoad(gameObject);
            await SceneManager.LoadSceneAsync(SceneManager.sceneCountInBuildSettings - 1);
        }
        
        private async UniTask LoadWithoutLoaderSceneAsync() => await SceneManager.LoadSceneAsync(Title);
        
        #endregion

        #region 
        
        public void Load()
        {
            if (_useLoadingScene)
                LoadWithLoaderScene();
            else
                LoadWithoutLoaderScene();
        }

        private void LoadWithLoaderScene()
        {
            DontDestroyOnLoad(gameObject);
            SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings - 1);
        }
        
        private void LoadWithoutLoaderScene() => SceneManager.LoadScene(Title);

        #endregion

        public void SetScene(int index) 
        {
            _gameScenes.IndexScene = (Scenes)index;
            Title = _gameScenes.GetTitleScene();
        }
        
        public void DestroySelf() => Destroy(gameObject);
    }
}