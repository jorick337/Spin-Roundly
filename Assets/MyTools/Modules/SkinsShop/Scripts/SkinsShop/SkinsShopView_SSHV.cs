using Cysharp.Threading.Tasks;
using MyTools.UI.Objects;
using UnityEngine;

namespace MyTools.Shop.Skins
{
    public class SkinsShopView_SSHV : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private SSHV_Icon _shopIcon;
        [SerializeField] private ButtonsSelector _buttonsSelectorSkins;


        // Managers
        private SSHV_Manager _shopManager;

        private async void Awake()
        {
            _shopManager = SSHV_Manager.Instance;
            await InitializeAsync();
        }

        private async UniTask InitializeAsync()
        {
            _buttonsSelectorSkins.Select(_shopManager.NumberSelectedSkin - 1);
            await _shopIcon.Initialized;
            EnableAlphaCanvasGroup();
        }

        private void EnableAlphaCanvasGroup() => _canvasGroup.alpha = 1f;
    }
}