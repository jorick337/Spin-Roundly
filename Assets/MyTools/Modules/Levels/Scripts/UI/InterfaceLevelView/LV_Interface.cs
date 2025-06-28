using MyTools.Levels.Play;
using MyTools.Loading;
using MyTools.UI;
using UnityEngine;

namespace MyTools.Levels.UI.Interface
{
    public class LV_Interface : MonoBehaviour
    {
        [SerializeField] private LS_Button _homeButton;

        // Managers
        private GameLevelManager _gameLevelManager;

        private void Awake() => _gameLevelManager = GameLevelManager.Instance;

        private void OnEnable()
        {
            _gameLevelManager.OnFinish += HideHomeButton;
            _gameLevelManager.OnRestart += ShowHomeButton;
        }

        private void OnDisable()
        {
            _gameLevelManager.OnFinish -= HideHomeButton;
            _gameLevelManager.OnRestart -= ShowHomeButton;
        }

        private void ShowHomeButton() => _homeButton.gameObject.SetActive(true);
        private void HideHomeButton() => _homeButton.gameObject.SetActive(false);
    }
}