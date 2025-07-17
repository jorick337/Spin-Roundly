#if USE_LOCALIZATION
using MyTools.UI;
using UnityEngine;
using UnityEngine.Localization.Components;

namespace MyTools.Localization
{
    public class LocalizationToggle : MyButton
    {
        [Header("Localization")]
        [SerializeField] private LocalizeStringEvent _localizeStringEvent;
        [SerializeField] private string _keyOn = "Setting_On";
        [SerializeField] private string _keyOff = "Setting_Off";

        public override void ClickToggle(bool isOn)
        {
            _localizeStringEvent.StringReference.TableEntryReference = isOn ? _keyOn : _keyOff;
            _localizeStringEvent.RefreshString();
        }
    }
}
#endif