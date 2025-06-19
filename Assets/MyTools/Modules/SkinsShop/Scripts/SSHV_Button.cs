using MyTools.UI;
using UnityEngine;
using UnityEngine.UI;

namespace MyTools.Shop.Skins
{
    public class SSHV_Button : MyButton
    {
        [Header("Shop")]
        [SerializeField] private int _number;

        // Managers
        private SSHV_Manager _shopManager;

        private void Awake() => _shopManager = SSHV_Manager.Instance;

        private void ChangeSkin() => _shopManager.ChangeNumber(_number);

        public override void ClickButtonAsync()
        {
            PlayClickSound();
            ChangeSkin();
        }
    }
}