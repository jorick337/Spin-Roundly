using MyTools.PlayerSystem;
using UnityEngine;
using UnityEngine.UI;

namespace MyTools.Advertising
{
    public class AD_RewardButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Text _text;
        [SerializeField] private int _reward;

        // Managers
        private PlayerManager _playerManager;

        private void Awake() => Initialize();
        private void OnEnable() => _button.onClick.AddListener(Click);
        private void OnDisable() => _button.onClick.RemoveListener(Click);

        private void Initialize()
        {
            _playerManager = PlayerManager.Instance;
            SetReward();
        }

        private void Click()
        {
            GiveReward();
        }

        private void GiveReward() => _playerManager.Player.AddMoney(_reward);


        private void SetReward() => SetText($"+{_reward}");
        private void SetText(string text) => _text.text =  text;
    }
}