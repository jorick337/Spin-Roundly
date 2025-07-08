using MyTools.UI.Objects;
using UnityEngine;

namespace MyTools.Shop.Skins
{
    public class SkinsShopView_SSHV : MonoBehaviour
    {
        [SerializeField] private ButtonsSelector _buttonsSelectorSkins;

        // Managers
        private SSHV_Manager _shopManager;

        private void Awake() 
        {
            _shopManager = SSHV_Manager.Instance;
            Initialize();
        }

        private void Initialize()
        {
            int number = _shopManager.Number;
            _shopManager.ChangeNumber(number);
            _buttonsSelectorSkins.Select(number - 1);
        }
    }
}