using UnityEngine;
using UnityEngine.Localization.Components;

namespace Game.Localization
{
    public class ToggleTextLocalization : MonoBehaviour
    {
        [Header("Core")]
        [SerializeField] private LocalizeStringEvent _localizeStringEvent;

        [Header("Keys")]
        [SerializeField] private string _keyOn = "Setting_On";
        [SerializeField] private string _keyOff = "Setting_Off";

        public void SetToggleState(bool isOn)
        {
            _localizeStringEvent.StringReference.TableEntryReference = isOn ? _keyOn : _keyOff;
            _localizeStringEvent.RefreshString();
        }
    }
}