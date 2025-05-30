using Cysharp.Threading.Tasks;
using MyTools.Loading;
using MyTools.PlayerSystem;
using MyTools.UI;
using MyTools.UI.Animation;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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
        [SerializeField] private MyButton _reloadButton;
        [SerializeField] private MyButton _homeButton;
        [SerializeField] private MyButton _forwardButton;

        // Managers
        private GameLevelManager _gameLevelManager;
        private PlayerManager _playerManager;
        private LoadScene _loadScene;
        private VictoryViewProvider _victoryViewProvider;

        #endregion

        #region MONO

        private void Awake()
        {
            _gameLevelManager = GameLevelManager.Instance;
            _playerManager = PlayerManager.Instance;
            _loadScene = LoadScene.Instance;
            SetTextMoney();
        }

        private void OnEnable()
        {
            _reloadButton.OnPressEnded += Reload;
            _homeButton.OnPressEnded += LoadStartScene;
            _forwardButton.OnPressEnded += LoadNextLevel;
        }

        private void OnDisable()
        {
            _reloadButton.OnPressEnded -= Reload;
            _homeButton.OnPressEnded -= LoadStartScene;
            _forwardButton.OnPressEnded -= LoadNextLevel;
        }

        private void Start() 
        {
            DisableUI();
            ShowStars();
            EnableUI();
        } 

        #endregion

        #region UI

        private async void ShowStars()
        {
            for (int i = 0; i < _gameLevelManager.Stars; i++)
                await _animationTranparencyStars[i].AnimateInAsync();
        }

        private void SetTextMoney() => _moneyText.text = _playerManager.Player.Money.ToString();

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

        private async UniTask Reload() => await InvokeActionAndUnload(() =>  _gameLevelManager.Restart());
        private async UniTask LoadStartScene() => await InvokeActionAndUnload(() =>  _loadScene.Load());
        private async UniTask LoadNextLevel() => await InvokeActionAndUnload(() =>  _gameLevelManager.Next());

        #endregion
    }
}