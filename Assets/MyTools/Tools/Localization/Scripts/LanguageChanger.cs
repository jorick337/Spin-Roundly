using UnityEngine;
using UnityEngine.UI;

namespace Game.Localization
{
    public class LanguageChanger : MonoBehaviour
    {
        [Header("Core")]
        [SerializeField] private Button button;

        // Managers
        private LanguageManager _languageManager;

        private void Awake()
        {
            _languageManager = LanguageManager.Instance;
        }

        private void OnEnable()
        {
            button.onClick.AddListener(_languageManager.ChangeLanguage);
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(_languageManager.ChangeLanguage);
        }
    }
}