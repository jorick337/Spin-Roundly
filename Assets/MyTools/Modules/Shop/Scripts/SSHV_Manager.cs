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

        private bool[] _activities;
        private int _number;
        private SSHV_Skin _skin;

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
            _number = SSHV_Saver.LoadNumberSkin();
            UpdateSkin();
        }

        #region CORE LOGIC

        public void ChangeNumber(int number)
        {
            _number = number;
            UpdateSkin();
        }

        public void BuySkin()
        {
            if (_activities[_number - 1] && _playerManager.Player.AddMoney(_skin.Price))
            {
                SaveNumber();
                SaveActivities();
                InvokeSkinChanged();
            }
        }

        private async void UpdateSkin()
        {
            _skin = await _skinProvider.Load(_number);
            SaveNumber();
            InvokeSpriteChanged();
            InvokePriceChanged();
            InvokeSkinChanged();
        }

        private void SaveNumber()
        {
            if (_activities[_number - 1])
                SSHV_Saver.SaveNumber(_number);
        }

        private void SaveActivities() => SSHV_Saver.SaveSkins(_activities);

        #endregion

        private void InvokeSpriteChanged() => SpriteChanged?.Invoke(_skin.Sprite);
        private void InvokePriceChanged() => PriceChanged?.Invoke(_skin.Price);
        private void InvokeSkinChanged() => SkinChanged?.Invoke(_activities[_number - 1]);
    }
}