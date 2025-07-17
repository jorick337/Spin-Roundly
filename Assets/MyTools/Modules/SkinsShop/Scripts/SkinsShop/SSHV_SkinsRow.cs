using MyTools.Levels;
using UnityEngine;
using UnityEngine.UI;

namespace MyTools.Shop.Skins
{
    public class SSHV_SkinsRow : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private GameObject _lockObject;
        [SerializeField] private Text _text;
        [SerializeField] private int _necessaryStars;

        // Managers
        private LevelsManager _levelsManager;

        private void Awake() => Initialize();

        private async void Initialize()
        {
            _levelsManager = LevelsManager.Instance;
            await _levelsManager.Initialized;
            Apply();
        }

        private void Apply()
        {
            int stars =_levelsManager.GetStars();

            if (stars >= _necessaryStars)
            {
                EnableCanvasGroup();
                DisableRow();
            }
            else
                SetText(_necessaryStars.ToString());
        }

        private void SetText(string text) => _text.text = text;
        private void EnableCanvasGroup() => _canvasGroup.interactable = true;
        private void DisableRow() => _lockObject.SetActive(false);
    }
}