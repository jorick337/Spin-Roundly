using MyTools.PlayerSystem;
using MyTools.UI.Objects;

namespace MyTools.Levels.UI.Counters
{
    public class MoneyCounter : Counter
    {
        private PlayerManager _playerManager;

        private void Awake()
        {
            _playerManager = PlayerManager.Instance;
            Initialize();
        } 

        private void OnEnable() => _playerManager.Player.MoneyChanged += UpdateStars;
        private void OnDisable() => _playerManager.Player.MoneyChanged -= UpdateStars;

        private void Initialize() => UpdateStars(_playerManager.Player.Money);

        private void UpdateStars(int money) => UpdateText(money.ToString());
    }
}