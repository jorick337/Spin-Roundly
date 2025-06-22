using Cysharp.Threading.Tasks;
using MyTools.PlayerSystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

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

        private bool _isBought = true;
        private int _selectedNumber = 1;
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

        private void OnEnable()
        {
            _playerManager.OnLoaded += Initialize;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable() 
        {
           _playerManager.OnLoaded -= Initialize; 
           SceneManager.sceneLoaded -= OnSceneLoaded;
        } 

        #region INITIALIZATION

        private async void Initialize()
        {
            Load();
            await UpdateSkin();
            _isLoaded = true;
        }

        private void Load()
        {
            Activities = SSHV_Saver.LoadSkins();
            Number = SSHV_Saver.LoadNumberSkin();
            _selectedNumber = Number;
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

        private async UniTask ChangeSkinToBoughtAsync() => Skin = await _skinProvider.Load(Number);
        private bool IsSelectedSkinBought() => Activities[_selectedNumber - 1];

        #endregion

        private async void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode) => await ChangeSkinToBoughtAsync();

        private void InvokeOnSpriteChanged() => OnSpriteChanged?.Invoke(Skin.Sprite);
        private void InvokeOnSpritePurchased() => OnSpritePurchased?.Invoke(Skin.Sprite);

        private void InvokePriceChanged() => PriceChanged?.Invoke(Skin.Price);
        private void InvokeSkinChanged() => SkinChanged?.Invoke(Activities[_selectedNumber - 1]);
    }
}