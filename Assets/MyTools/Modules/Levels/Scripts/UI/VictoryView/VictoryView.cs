using Cysharp.Threading.Tasks;
using MyTools.Advertising;
using MyTools.PlayerSystem;
using MyTools.UI;
using MyTools.UI.Animation;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YG;

namespace MyTools.Levels.Play
{
    public class VictoryView : MonoBehaviour
    {
        #region CORE

        [Header("Core")]
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private AnimationTransparency[] _animationTranparencyStars;
        [SerializeField] private Text _moneyText;

        [Header("Buttons")]
        [SerializeField] private AD_RewardScrollbar _rewardScrollbar;
        [SerializeField] private MyButton _reloadButton;
        [SerializeField] private MyButton _forwardButton;

        // Managers
        private GameLevelManager _gameLevelManager;
        private PlayerManager _playerManager;
        private VictoryViewProvider _victoryViewProvider;

        #endregion

        #region MONO

        private void Awake()
        {
            _gameLevelManager = GameLevelManager.Instance;
            _playerManager = PlayerManager.Instance;
            SetTextMoney();
        }

        private void OnEnable()
        {
            _reloadButton.OnPressEnded += Reload;
            _forwardButton.OnPressEnded += LoadNextLevel;
            _gameLevelManager.CollectedMoneyChanged += UpdateReward;
        }

        private void OnDisable()
        {
            _reloadButton.OnPressEnded -= Reload;
            _forwardButton.OnPressEnded -= LoadNextLevel;
            _gameLevelManager.CollectedMoneyChanged -= UpdateReward;
        }

        private async void Start()
        {
            DisableUI();
            UpdateReward(_gameLevelManager.CollectedMoney);
            await ShowStars();
            EnableRewardScrollbar();
            EnableUI();
        }

        #endregion

        #region UI

        private async UniTask ShowStars()
        {
            for (int i = 0; i < _gameLevelManager.Stars; i++)
            {
                await _animationTranparencyStars[i].AnimateInAsync();
                _gameLevelManager.AddMoney(10);
            }
        }

        private void SetTextMoney() => _moneyText.text = _playerManager.Player.Money.ToString();

        private void EnableRewardScrollbar() => _rewardScrollbar.Initialize();
        private void EnableUI() => _canvasGroup.interactable = true;
        private void DisableUI() => _canvasGroup.interactable = false;

        #endregion

        #region VALUES

        public void SetProvider(VictoryViewProvider victoryViewProvider) => _victoryViewProvider = victoryViewProvider;

        private async UniTask InvokeActionAndUnload(UnityAction action)
        {
            DisableUI();
            action?.Invoke();
            await _victoryViewProvider.UnloadAsync();
        }

        #endregion

        #region CALLBACKS

        private void UpdateReward(int reward) => _rewardScrollbar.SetInitialReward(reward);

        private async UniTask Reload() => await InvokeActionAndUnload(() => _gameLevelManager.Restart());
        private async UniTask LoadNextLevel() => await InvokeActionAndUnload(() =>
        {
            YG2.InterstitialAdvShow();
            _gameLevelManager.Next();
        });

        #endregion
    }
}