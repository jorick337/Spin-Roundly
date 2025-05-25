using Cysharp.Threading.Tasks;
using MyTools.Music;
using MyTools.UI;
using MyTools.UI.Animate;
using UnityEngine;

namespace MyTools.Start
{
    public class StartView : MonoBehaviour
    {
        #region CORE

        [Header("Core")]
        [SerializeField] protected CanvasGroup _canvasGroup;
        [SerializeField] protected MyButton _shopButton;
        [SerializeField] protected MyButton _leaderboardButton;

        // Managers
        protected MusicManager _musicManager;

        #endregion

        #region MONO

        private void Awake() => _musicManager = MusicManager.Instance;

        private void OnEnable()
        {
            _shopButton.OnPressed += ClearStartView;
            _shopButton.OnPressEnded += LoadShopView;
            _leaderboardButton.OnPressed += ClearStartView;
            _leaderboardButton.OnPressEnded += LoadLeaderboardView;
        }

        private void OnDisable()
        {
            _shopButton.OnPressed -= ClearStartView;
            _shopButton.OnPressEnded -= LoadShopView;
            _leaderboardButton.OnPressed -= ClearStartView;
            _leaderboardButton.OnPressEnded -= LoadLeaderboardView;
        }

        #endregion

        #region UI

        protected void EnableUI() => _canvasGroup.interactable = true;
        protected void DisableUI() => _canvasGroup.interactable = false;

        #endregion

        #region CALLBACKS

        protected async UniTask ClearStartView(AnimateScaleXInUI animateScaleXInUI)
        {
            DisableUI();
            PlayClickSound();
            await animateScaleXInUI.AnimateAsync();
        }

        private async UniTask LoadShopView() => await UniTask.CompletedTask;
        private async UniTask LoadLeaderboardView() => await UniTask.CompletedTask;

        private void PlayClickSound() => _musicManager.PlayClickSound();

        #endregion
    }
}