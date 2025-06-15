using Cysharp.Threading.Tasks;
using MyTools.PlayerSystem;
using MyTools.UI;
using UnityEngine;
using UnityEngine.UI;

namespace MyTools.Shop.Skins
{
    public class SkinsShopView_SSHV : MonoBehaviour
    {
        public static SkinsShopView_SSHV Instance { get; private set; }

        [SerializeField] private MyButton _closeButton;
        [SerializeField] private Image _iconImage;
        [SerializeField] private Text _costText;
        [SerializeField] private GameObject _costObject;

        public bool[] ActivitySkins { get; private set; }

        // Managers
        private SSHV_Provider _provider;

        private void Awake() => Initialize();
        private void OnEnable() => _closeButton.OnPressed += Unload;
        private void OnDisable() => _closeButton.OnPressed -= Unload;

        private void Initialize()
        {
            Instance = this;
            ActivitySkins = SaveManager.LoadSkins();
        }

        public void ChangeSkin(Sprite sprite, int cost, bool activeCost)
        {
            _iconImage.sprite = sprite;
            _costText.text = cost.ToString();
            _costObject.SetActive(activeCost);
        }

        public void SetProvider(SSHV_Provider provider) => _provider = provider;

        private async UniTask Unload() => await _provider.UnloadAsync();
    }
}