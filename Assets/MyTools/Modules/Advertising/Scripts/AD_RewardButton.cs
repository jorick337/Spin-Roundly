using MyTools.PlayerSystem;
using UnityEngine;
using UnityEngine.UI;

namespace MyTools.Advertising
{
    public class AD_RewardButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private int _reward;

        // Managers
        private PlayerManager _playerManager;

        private void Awake() => _playerManager = PlayerManager.Instance;
        private void OnEnable() => _button.onClick.AddListener(Click);
        private void OnDisable() => _button.onClick.RemoveListener(Click);

        private void Click()
        {
            Reward();
        }

        private void Reward() => _playerManager.Player.AddMoney(_reward);
    }
}