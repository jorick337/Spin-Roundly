using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MyTools.Loading
{
    public class LoadingScene : MonoBehaviour
    {
        [Header("Core")]
        [SerializeField] private Text progressText;
        [SerializeField] private Slider progressSlider;

        private LoadScene _loadScene;

        private void Awake()
        {
            _loadScene = LoadScene.Instance;
        }

        private void Start()
        {
            StartCoroutine(Load());
        }

        #region CORE LOGIC

        IEnumerator Load()
        {
            yield return new WaitForSeconds(0.1f);

            StartCoroutine(LoadAsync());
            _loadScene.DestroySelf();
        }

        IEnumerator LoadAsync()
        {
            if (_loadScene.IsAddressables)
            {
                var handle = Addressables.LoadSceneAsync(_loadScene.Title);
                
                while (!handle.IsDone)
                {
                    float progress = Mathf.Clamp01(handle.PercentComplete);

                    progressSlider.value = progress;
                    progressText.text = $"{progress * 100:F0}%";

                    yield return null;
                }
            }
            else
            {
                AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(_loadScene.Title);
                asyncLoad.allowSceneActivation = false;

                while (!asyncLoad.isDone)
                {
                    float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);

                    progressSlider.value = progress;
                    progressText.text = $"{progress * 100:F0}%";

                    if (asyncLoad.progress >= 0.9f)
                    {
                        asyncLoad.allowSceneActivation = true;
                    }

                    yield return null;
                }
            }
        }

        #endregion
    }
}