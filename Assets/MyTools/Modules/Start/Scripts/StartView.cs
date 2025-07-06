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

        #endregion

        #region MONO

        private void OnEnable()
        {
            _shopButton.OnPressEnded += LoadShopView;
        }

        private void OnDisable()
        {
            _shopButton.OnPressEnded -= LoadShopView;
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
            ShopProvider provider = new();
            await provider.Load(transform.parent);
        }

        #endregion
    }
}