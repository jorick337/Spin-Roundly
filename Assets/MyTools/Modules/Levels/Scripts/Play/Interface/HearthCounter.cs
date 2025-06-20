using Cysharp.Threading.Tasks;
using MyTools.Levels.TwoDimensional.Objects.Health;
using MyTools.UI;
using UnityEngine;

namespace MyTools.Levels.Play
{
    public class HearthCounter : Counter
    {
        [SerializeField] private Health_HL2 _playerHealth;
        [SerializeField] private bool _findInstance = false;

        // Managers
        private GameLevelManager _gameLevelManager;

        private async void Awake()
        {
            _gameLevelManager = GameLevelManager.Instance;
            await _gameLevelManager.WaitUntilLoaded();
            Initialize();
            UpdateHealth();
        }

        private void OnEnable() => _gameLevelManager.OnRestart += UpdateHealth;

        private void OnDisable()
        {
            _playerHealth.OnChanged -= UpdateHealth;
            _gameLevelManager.OnRestart -= UpdateHealth;
        }

        private void Initialize()
        {
            if (_findInstance)
                _playerHealth = Health_HL2.Instance;

            _playerHealth.OnChanged += UpdateHealth;
        }

        private void UpdateHealth() => UpdateHealth(_playerHealth.Current);
        private void UpdateHealth(int health) => UpdateText(health.ToString());
    }
}