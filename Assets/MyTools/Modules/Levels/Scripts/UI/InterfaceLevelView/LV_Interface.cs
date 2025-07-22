using MyTools.Levels.Play;
using MyTools.Levels.TwoDimensional.Objects.Health;
using MyTools.Movement.ThreeDimensional.UI;
using UnityEngine;
using YG;

namespace MyTools.Levels.UI.Interface
{
    public class LV_Interface : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;

        private GameLevelManager _gameLevelManager;
        private Health_HL2 _healthPlayer;
        private UIMovementProvider _uIMovementProvider = new();

        private async void Awake()
        {
            _gameLevelManager = GameLevelManager.Instance;

            if (YG2.envir.isMobile)
                LoadUIMovement();

            await _gameLevelManager.WaitUntilLoaded();
            Restart();
        }

        private void OnEnable()
        {
            _gameLevelManager.OnFinish += HideUI;
            _gameLevelManager.OnRestart += Restart;
        }

        private void OnDisable()
        {
            _gameLevelManager.OnFinish -= HideUI;
            _gameLevelManager.OnRestart -= Restart;
        }

        private void Restart()
        {
            ShowUI();

            _healthPlayer = Health_HL2.Instance;
            _healthPlayer.OnDead += HideUI;
        }

        private void ShowUI()
        {
            _canvasGroup.alpha = 1f;
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.interactable = true;
        }

        private void HideUI()
        {
            _canvasGroup.alpha = 0f;
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.interactable = false;
        }
        
        private void LoadUIMovement() => _uIMovementProvider.Load(_canvasGroup.transform);
    }
}