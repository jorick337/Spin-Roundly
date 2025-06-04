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

        private void Awake()
        {
            _gameLevelManager = GameLevelManager.Instance;
            InitializeAsync();
        }

        private void OnEnable()
        {
            if (!_findInstance)
                RegisterEvents();
        }

        private void OnDisable()
        {
            _playerHealth.Changed -= UpdateHealth;
            _playerHealth.Dead -= Restart;
            _gameLevelManager.OnRestart -= InitializeAsync;
        }

        private async void InitializeAsync()
        {
            await _gameLevelManager.WaitUntilLoaded();

            if (_findInstance)
            {
                _playerHealth = Health.Instance;
                RegisterEvents();
            }

            UpdateHealth(_playerHealth.Current);
        }

        private void RegisterEvents()
        {
            _playerHealth.Changed += UpdateHealth;
            _playerHealth.Dead += Restart;
            _gameLevelManager.OnRestart += InitializeAsync;
        }

        private void UpdateHealth(int health) => UpdateText(health.ToString());

        private void Restart() 
        {
            _playerHealth.Restart();
            _gameLevelManager.Restart();
        } 
    }
}