using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MyTools.Shop.Skins
{
    public class SSHV_Button : MonoBehaviour
    {
        [Header("Click")]
        [SerializeField] Button _button;
        [SerializeField] AudioSource _audioSource;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private int _cost;
        [SerializeField] private int _number;

        // Managers
        private SkinsShopView_SSHV _shopView;

        private void OnEnable() => _button.onClick.AddListener(Click);
        private void OnDisable() => _button.onClick.RemoveListener(Click);
        private void Start() => _shopView = SkinsShopView_SSHV.Instance; 

        private void PlayClickSound() => _audioSource.Play();
        private void ChangeSkin() => _shopView.ChangeSkin(_sprite, _cost, !_shopView.ActivitySkins[_number - 1]);

        private void Click()
        {
            ChangeSkin();
            PlayClickSound();
        }
    }
}