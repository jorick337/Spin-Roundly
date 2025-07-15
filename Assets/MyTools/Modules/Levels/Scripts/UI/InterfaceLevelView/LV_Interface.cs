using MyTools.Levels.Play;
using MyTools.Movement.ThreeDimensional.UI;
using UnityEngine;
using YG;

namespace MyTools.Levels.UI.Interface
{
    public class LV_Interface : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;

        private GameLevelManager _gameLevelManager;

        private void Awake()
        {
            _gameLevelManager = GameLevelManager.Instance;

            if (YG2.envir.isMobile)
                LoadUIMovement();
        }

        private void OnEnable()
        {
            _gameLevelManager.OnFinish += HideUI;
            _gameLevelManager.OnRestart += ShowUI;
        }

        private void OnDisable()
        {
            _gameLevelManager.OnFinish -= HideUI;
            _gameLevelManager.OnRestart -= ShowUI;
        }

        private void LoadUIMovement()
        {
            UIMovementProvider provider = new();
            provider.Load(_canvasGroup.transform);
        }

        private void ShowUI()
        {
            _canvasGroup.alpha = 1f;
            _canvasGroup.interactable = true;
        }

        private void HideUI()
        {
            _canvasGroup.alpha = 0f;
            _canvasGroup.interactable = false;
        }
    }
}