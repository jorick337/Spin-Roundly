using UnityEngine;
using UnityEngine.UI;

namespace Game.Localization
{
    public class LanguageChanger : MonoBehaviour
    {
        [SerializeField] private Button button;

        // Managers
        private LanguageManager _languageManager;

        private void Awake() => _languageManager = LanguageManager.Instance;
        private void OnEnable() => button.onClick.AddListener(ChangeLanguage);
        private void OnDisable() => button.onClick.RemoveListener(ChangeLanguage);

        private void ChangeLanguage() => _languageManager.ChangeLanguage();
    }
}