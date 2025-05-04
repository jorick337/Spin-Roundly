using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;

namespace Game.Localization
{
    public class LanguageManager : MonoBehaviour
    {

        private readonly string[] LOCALES = new string[2] { "ru", "en" };

        public Action LocaleChanged;

        public static LanguageManager Instance { get; private set; }

        public string Locale { get; private set; }

        private int _localeIndex;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public IEnumerator Initialize(string locale)
        {
            yield return LocalizationSettings.InitializationOperation;

            _localeIndex = Array.IndexOf(LOCALES, locale);
            Locale = locale;
            UpdateLocale();
        }

        public void ChangeLanguage()
        {
            _localeIndex = (_localeIndex + 1) % LOCALES.Length;
            Locale = LOCALES[_localeIndex];

            UpdateLocale();
            LocaleChanged?.Invoke();
        }

        private void UpdateLocale()
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales
                .Find(l => l.Identifier.Code == Locale);
        }
    }
}