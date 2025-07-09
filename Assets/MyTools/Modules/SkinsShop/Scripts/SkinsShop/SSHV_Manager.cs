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

        public bool[] Activities { get; private set; }
        public int Number { get; private set; } = 1;
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
            Activities = SSHV_Saver.LoadSkins();
            Number = SSHV_Saver.LoadNumberSkin();

            await LoadBoughtSkin();
            _selectedNumber = Number;

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
            if (Skin != null)
                await _skinProvider.UnloadAsync();

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
                Number = _selectedNumber;
                SSHV_Saver.SaveNumber(Number);
            }
        }

        private void SaveActivities()
        {
            Activities[_selectedNumber - 1] = true;
            _isBought = true;
            SSHV_Saver.SaveSkins(Activities);
            InvokeSkinChanged();
        }

        #endregion

        #region VALUES

        public async UniTask WaitUntilLoaded() => await UniTask.WaitUntil(() => _isLoaded == true);
        public async UniTask LoadBoughtSkin()
        {
            if (_selectedNumber != Number)
            {
                if (Skin != null)
                    await _skinProvider.UnloadAsync();
                
                Skin = await _skinProvider.Load(Number);
            }
        }

        private bool IsSelectedSkinBought() => Activities[_selectedNumber - 1];

        #endregion

        private void InvokeOnSpriteChanged() => OnSpriteChanged?.Invoke(Skin.Sprite);
        private void InvokeOnSpritePurchased() => OnSpritePurchased?.Invoke(Skin.Sprite);

        private void InvokePriceChanged() => PriceChanged?.Invoke(Skin.Price);
        private void InvokeSkinChanged() => SkinChanged?.Invoke(Activities[_selectedNumber - 1]);
    }
}