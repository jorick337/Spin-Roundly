using Cysharp.Threading.Tasks;
using MyTools.PlayerSystem;
using UnityEngine;
using UnityEngine.Events;

namespace MyTools.Shop.Skins
{
    public class SSHV_Manager : MonoBehaviour
    {
        public event UnityAction<Sprite> OnSpriteChanged;
        public event UnityAction<Sprite> OnSpritePurchased;

        public event UnityAction<int> PriceChanged;
        public event UnityAction<bool> SkinChanged;

        public static SSHV_Manager Instance { get; private set; }
        public int Number { get; private set; }

        private SSHV_Skin _skin;
        private bool[] _activities;
        private bool _isBought = true;
        private int _selectedNumber = 1;

        // Managers
        private PlayerManager _playerManager;
        private SSHV_SkinProvider _skinProvider;

        private void Awake()
        {
            Instance = this;
            _playerManager = PlayerManager.Instance;
            _skinProvider = new();
        }

        private void OnEnable() => _playerManager.OnLoaded += Initialize;
        private void OnDisable() => _playerManager.OnLoaded -= Initialize;

        private async void Initialize()
        {
            _activities = SSHV_Saver.LoadSkins();
            Number = SSHV_Saver.LoadNumberSkin();
            await UpdateSkin();
        }

        #region CORE LOGIC

        public async void ChangeNumber(int number)
        {
            _selectedNumber = number;
            _isBought = IsSelectedSkinBought();

            if (_isBought)
                SaveNumber();

            await UpdateSkin();
        }

        public async void BuySkin()
        {
            if (!_isBought && _playerManager.Player.AddMoney(-_skin.Price))
            {
                SaveActivities();
                SaveNumber();
                await UpdateSkin();
            }
        }

        private async UniTask UpdateSkin()
        {
            _skin = await _skinProvider.Load(_selectedNumber);

            if (_isBought)
                InvokeOnSpritePurchased();
            else
                InvokeOnSpriteChanged();

            InvokePriceChanged();
            InvokeSkinChanged();
        }

        private void SaveNumber()
        {
            if (_isBought)
            {
                Number = _selectedNumber;
                SSHV_Saver.SaveNumber(Number);
            }
        }

        private void SaveActivities()
        {
            _activities[_selectedNumber - 1] = true;
            _isBought = true;
            SSHV_Saver.SaveSkins(_activities);
        }

        #endregion

        private bool IsSelectedSkinBought() => _activities[_selectedNumber - 1];

        private void InvokeOnSpriteChanged() => OnSpriteChanged?.Invoke(_skin.Sprite);
        private void InvokeOnSpritePurchased() => OnSpritePurchased?.Invoke(_skin.Sprite);

        private void InvokePriceChanged() => PriceChanged?.Invoke(_skin.Price);
        private void InvokeSkinChanged() => SkinChanged?.Invoke(_activities[_selectedNumber - 1]);
    }
}