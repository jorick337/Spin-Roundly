using UnityEngine;
using UnityEngine.Localization.Components;

namespace Game.Localization
{
    public class ToggleTextLocalization : MonoBehaviour
    {
        [Header("Core")]
        [SerializeField] private LocalizeStringEvent localizedText;

        [Header("Keys")]
        [SerializeField] private string keyOn = "Setting_On";
        [SerializeField] private string keyOff = "Setting_Off";

        public void SetToggleState(bool isOn)
        {
            localizedText.StringReference.TableEntryReference = isOn ? keyOn : keyOff;
            localizedText.RefreshString();
        }
    }
}