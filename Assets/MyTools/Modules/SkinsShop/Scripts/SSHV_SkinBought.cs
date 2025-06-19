using UnityEngine;
using UnityEngine.UI;

namespace MyTools.Shop.Skins
{
    public class SSHV_SkinBought : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private int _number;

        // Managers
        private SSHV_Manager _shopManager;

        private void Awake() => Initialize();
        private void OnEnable() => _shopManager.SkinChanged += EnableImage;
        private void OnDisable() => _shopManager.SkinChanged -= EnableImage;

        private void Initialize()
        {
            _shopManager = SSHV_Manager.Instance;
            EnableImage(_shopManager.Activities[_number - 1]);
        }

        private void EnableImage(bool active) => _image.enabled = true;
    }
}