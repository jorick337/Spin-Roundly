using UnityEngine;
using UnityEngine.UI;

namespace MyTools.Shop.Skins
{
    public class SSHV_SkinBought : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private int _number;

        private bool _active = false;

        // Managers
        private SSHV_Manager _shopManager;

        private void Awake() => Initialize();

        private void OnEnable()
        {
            if (!_active)
                _shopManager.SkinChanged += CheckNumberSkin;
        }

        private void OnDisable() => _shopManager.SkinChanged -= CheckNumberSkin;

        private void Initialize()
        {
            _shopManager = SSHV_Manager.Instance;
            CheckNumberSkin();
        }

        private void CheckNumberSkin()
        {
            _active = _shopManager.Activities[_number - 1];
            if (_active)
            {
                EnableImage(_active);
                OnDisable();
            }
        }

        private void EnableImage(bool active) => _image.enabled = active;

        private void CheckNumberSkin(bool active) => CheckNumberSkin();
    }
}