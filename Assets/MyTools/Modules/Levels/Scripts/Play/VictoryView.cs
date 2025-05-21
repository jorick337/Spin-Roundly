using Cysharp.Threading.Tasks;
using MyTools.Music;
using MyTools.PlayerSystem;
using MyTools.UI;
using MyTools.UI.Animate;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MyTools.Levels.Play
{
    public class VictoryView : MonoBehaviour
    {
        #region EVENTS

        public event UnityAction OnReloadPressed;
        public event UnityAction OnHomePressed;
        public event UnityAction OnForwardPressed;

        #endregion

        #region CORE

        [Header("Core")]
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private AnimateTranparencyInUI[] _animateTranparencyInStars;
        [SerializeField] private Text _moneyText;

        [Header("Buttons")]
        [SerializeField] private MyButton _reloadButton;
        [SerializeField] private MyButton _homeButton;
        [SerializeField] private MyButton _forwardButton;

        // Managers
        private PlayerManager _playerManager;
        private GameLevelManager _gameLevelView;
        private MusicManager _musicManager;
        private VictoryViewProvider _victoryViewProvider;

        #endregion

        #region MONO

        private void Awake()
        {
            _playerManager = PlayerManager.Instance;
            _gameLevelView = GameLevelManager.Instance;
            _musicManager = MusicManager.Instance;
            SetTextMoney();
        }

        private void OnEnable()
        {
            _reloadButton.OnPressed += DisableUIAndClick;
            _reloadButton.OnPressEnded += InvokeOnReloadPressed;
            _homeButton.OnPressed += DisableUIAndClick;
            _homeButton.OnPressEnded += InvokeOnHomePressed;
            _forwardButton.OnPressed += DisableUIAndClick;
            _forwardButton.OnPressEnded += InvokeOnForwardPressed;
        }

        private void OnDisable()
        {
            _reloadButton.OnPressed -= DisableUIAndClick;
            _reloadButton.OnPressEnded -= InvokeOnReloadPressed;
            _homeButton.OnPressed -= DisableUIAndClick;
            _homeButton.OnPressEnded -= InvokeOnHomePressed;
            _forwardButton.OnPressed -= DisableUIAndClick;
            _forwardButton.OnPressEnded -= InvokeOnForwardPressed;
        }

        private void Start() => ShowStars();

        #endregion

        #region UI

        private async void ShowStars()
        {
            for (int i = 0; i < _gameLevelView.NumberStars; i++)
                await _animateTranparencyInStars[i].AnimateInAsync();
        }

        private void SetTextMoney() => _moneyText.text = _playerManager.Player.Money.ToString();
        private void DisableUI() => _canvasGroup.interactable = false;

        #endregion

        #region VALUES

        public void SetProvider(VictoryViewProvider victoryViewProvider) => _victoryViewProvider = victoryViewProvider;

        private async UniTask InvokeAction(UnityAction action)
        {
            action?.Invoke();
            await _victoryViewProvider.UnloadAsync();
        }

        #endregion

        #region CALLBACKS

        private async UniTask DisableUIAndClick(AnimateScaleXInUI animateScaleXInUI)
        {
            DisableUI();
            PlayClickSound();
            await animateScaleXInUI.AnimateAsync();
        }

        private async UniTask InvokeOnReloadPressed() => await InvokeAction(OnReloadPressed);
        private async UniTask InvokeOnHomePressed() => await InvokeAction(OnHomePressed);
        private async UniTask InvokeOnForwardPressed() => await InvokeAction(OnForwardPressed);

        private void PlayClickSound() => _musicManager.PlayClickSound();

        #endregion
    }
}