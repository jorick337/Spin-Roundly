using UnityEngine;
using UnityEngine.UI;

namespace MyTools.Loading
{
    public class LS_Button : MonoBehaviour
    {
        [SerializeField] private Button _button;

        // Manager
        private LoadScene _loadScene;

        private void Awake() => _loadScene = LoadScene.Instance;
        private void OnEnable() => _button.onClick.AddListener(Load);
        private void OnDisable() => _button.onClick.RemoveListener(Load);

        private void Load() => _loadScene.Load();
    }
}