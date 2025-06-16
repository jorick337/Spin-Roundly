using Cysharp.Threading.Tasks;
using MyTools.Shop.Skins;
using MyTools.UI;
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

        #endregion

        #region MONO

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

        protected async UniTask ClearStartView() 
        {
            DisableUI();
            await UniTask.CompletedTask;
        } 

        private async UniTask LoadShopView() 
        {
            SSHV_Provider provider = new();
            await provider.Load();
        }

        private async UniTask LoadLeaderboardView() => await UniTask.CompletedTask;

        #endregion
    }
}