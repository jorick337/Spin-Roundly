using System.Threading.Tasks;
using MyTools.UI;
using UnityEngine;

namespace MyTools.Levels.Play
{
    public class HearthCounter : Counter
    {
        [SerializeField] private Health _playerHealth;
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
            _playerHealth.Changed -= UpdateHealth;
            _gameLevelManager.OnRestart -= UpdateHealth;
        }

        private void Initialize()
        {
            if (_findInstance)
                _playerHealth = Health.Instance;

            _playerHealth.Changed += UpdateHealth;
        }

        private void UpdateHealth() => UpdateHealth(_playerHealth.Current);
        private void UpdateHealth(int health) => UpdateText(health.ToString());
    }
}