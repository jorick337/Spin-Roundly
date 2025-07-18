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

        public bool[] ActivitySkins { get; private set; }
        public int NumberSelectedSkin { get; private set; } = 1;
        public SSHV_Skin Skin { get; private set; }

        private int _selectedNumber = 0;
        private bool _isBought = true;
        private bool _isLoaded = false;

        // Managers
        private PlayerManager _playerManager;
        private SSHV_SkinProvider _skinProvider;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                _playerManager = PlayerManager.Instance;
                _skinProvider = new();
                DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(gameObject);
        }

        private void OnEnable() => _playerManager.OnLoaded += Initialize;

        private void OnDisable()
        {
            if (_isLoaded)
                _playerManager.OnLoaded -= Initialize;
        }

        #region INITIALIZATION

        private async void Initialize()
        {
            ActivitySkins = SaveManager.LoadActivitySkins();
            NumberSelectedSkin = SaveManager.LoadNumberSelectedSkin();

            await LoadBoughtSkin();
            _selectedNumber = NumberSelectedSkin;

            _isLoaded = true;
        }

        #endregion

        #region CORE LOGIC

        public async void ChangeNumber(int number)
        {
            _selectedNumber = number;
            _isBought = IsSelectedSkinBought();

            SaveNumber();
            await UpdateSkin();
        }

        public async void BuySkin()
        {
            if (!_isBought && _playerManager.Player.AddMoney(-Skin.Price))
            {
                SaveActivities();
                SaveNumber();
                await UpdateSkin();
            }
        }

        private async UniTask UpdateSkin()
        {
            Skin = await _skinProvider.Load(_selectedNumber);

            if (_isBought)
                InvokeOnSpritePurchased();
            else
                InvokeOnSpriteChanged();

            InvokePriceChanged();
            InvokeSkinChanged();
        }

        #endregion

        #region SAVING

        private void SaveNumber()
        {
            if (_isBought)
            {
                NumberSelectedSkin = _selectedNumber;
                SaveManager.SaveNumberSelectedSkin(NumberSelectedSkin);
            }
        }

        private void SaveActivities()
        {
            ActivitySkins[_selectedNumber - 1] = true;
            _isBought = true;
            SaveManager.SaveActivitySkins(ActivitySkins);
            InvokeSkinChanged();
        }

        #endregion

        #region VALUES

        public async UniTask WaitUntilLoaded() => await UniTask.WaitUntil(() => _isLoaded == true);
        public async UniTask LoadBoughtSkin()
        {
            if (_selectedNumber != NumberSelectedSkin)
            {
                if (Skin != null)
                    await _skinProvider.UnloadAllAsync();
                
                Skin = await _skinProvider.Load(NumberSelectedSkin);
            }
        }

        private bool IsSelectedSkinBought() => ActivitySkins[_selectedNumber - 1];

        #endregion

        private void InvokeOnSpriteChanged() => OnSpriteChanged?.Invoke(Skin.Sprite);
        private void InvokeOnSpritePurchased() => OnSpritePurchased?.Invoke(Skin.Sprite);

        private void InvokePriceChanged() => PriceChanged?.Invoke(Skin.Price);
        private void InvokeSkinChanged() => SkinChanged?.Invoke(ActivitySkins[_selectedNumber - 1]);
    }
}