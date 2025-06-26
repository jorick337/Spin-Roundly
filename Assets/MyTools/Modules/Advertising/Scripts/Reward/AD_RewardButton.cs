using MyTools.PlayerSystem;
using UnityEngine;
using UnityEngine.UI;

namespace MyTools.Advertising
{
    public class AD_RewardButton : MonoBehaviour
    {
        [Header("Core")]
        [SerializeField] private Button _button;
        [SerializeField] private Text _text;
        [SerializeField] private int _reward = 0;
        [SerializeField] private bool _canInitialize = false;
        [SerializeField] private bool _looping = true;

        // Managers
        private PlayerManager _playerManager;

        private void Awake()
        {
            _playerManager = PlayerManager.Instance;
            if (_canInitialize)
                Initialize();
        }

        private void OnEnable() => _button.onClick.AddListener(Click);
        private void OnDisable() => _button.onClick.RemoveListener(Click);

        private void Initialize() => SetReward(_reward);

        public void SetReward(int reward) 
        {
            _reward = reward;
            SetText($"+{reward}");
        }

        private void SetText(string text) => _text.text = text;
        private void GiveReward() => _playerManager.Player.AddMoney(_reward);
        private void DestroySelf() => Destroy(gameObject);

        private void Click()
        {
            GiveReward();
            if (!_looping)
                DestroySelf();
        }
    }
}