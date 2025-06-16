using MyTools.PlayerSystem;
using UnityEngine;
using UnityEngine.Events;

namespace MyTools.Shop.Skins
{
    public class SSHV_Manager : MonoBehaviour
    {
        public event UnityAction<Sprite> SpriteChanged;
        public event UnityAction<int> PriceChanged;
        public event UnityAction<bool> SkinChanged;

        public static SSHV_Manager Instance { get; private set; }
        public int Number { get; private set; }
        
        private bool[] _activities;
        private SSHV_Skin _skin;
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

        private void Initialize()
        {
            _activities = SSHV_Saver.LoadSkins();
            Number = SSHV_Saver.LoadNumberSkin();
            UpdateSkin();
        }

        #region CORE LOGIC

        public void ChangeNumber(int number)
        {
            _selectedNumber = number;

            if (IsSelectedSkinBought())
            {
                Number = _selectedNumber;
                SaveNumber();
            }

            UpdateSkin();
        }

        public void BuySkin()
        {
            if (!IsSelectedSkinBought() && _playerManager.Player.AddMoney(-_skin.Price))
            {
                _activities[_selectedNumber - 1] = true;
                SaveActivities();
                Number = _selectedNumber;
                SaveNumber();
                InvokeSkinChanged();
            }
        }

        private async void UpdateSkin()
        {
            _skin = await _skinProvider.Load(_selectedNumber);
            InvokeSpriteChanged();
            InvokePriceChanged();
            InvokeSkinChanged();
        }

        private void SaveNumber()
        {
            if (IsSelectedSkinBought())
                SSHV_Saver.SaveNumber(Number);
        }

        private void SaveActivities() => SSHV_Saver.SaveSkins(_activities);

        #endregion

        private bool IsSelectedSkinBought() => _activities[_selectedNumber - 1];

        private void InvokeSpriteChanged() => SpriteChanged?.Invoke(_skin.Sprite);
        private void InvokePriceChanged() => PriceChanged?.Invoke(_skin.Price);
        private void InvokeSkinChanged() => SkinChanged?.Invoke(_activities[_selectedNumber - 1]);
    }
}