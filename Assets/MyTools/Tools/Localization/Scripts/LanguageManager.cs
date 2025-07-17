#if USE_LOCALIZATION
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace Game.Localization
{
    public class LanguageManager : MonoBehaviour
    {
        public UnityAction<string> LocaleChanged;

        public static LanguageManager Instance { get; private set; }

        public Locale SelectedLocale { get; private set; }

        private Locale[] _locales;
        private int _index;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(gameObject);
        }

        public async void Initialize(string locale)
        {
            await LocalizationSettings.InitializationOperation.Task;

            _locales = LocalizationSettings.AvailableLocales.Locales.ToArray();
            SelectedLocale = Array.Find(_locales, l => l.Identifier.Code == locale);
            _index = Array.IndexOf(_locales, SelectedLocale);

            UpdateLocale();
        }

        public void ChangeLanguage()
        {
            _index = (_index + 1) % _locales.Length;
            UpdateLocale();
        }

        private void UpdateLocale()
        {
            SelectedLocale = _locales[_index];
            LocalizationSettings.SelectedLocale = SelectedLocale;
            InvokeLocaleChanged();
        }

        private void InvokeLocaleChanged() => LocaleChanged?.Invoke(SelectedLocale.Identifier.Code);
    }
}
#endif