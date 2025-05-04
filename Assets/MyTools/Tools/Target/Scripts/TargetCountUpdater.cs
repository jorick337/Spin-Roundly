using UnityEngine;
using UnityEngine.UI;

namespace Game.Target
{
    public class TargetCountUpdater : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private Text text;

        [Header("Managers")]
        [SerializeField] private TargetsManager targetsManager;

        private void OnEnable()
        {
            targetsManager.CountTargetsChanged += UpdateCountTargets;
        }

        private void OnDisable()
        {
            targetsManager.CountTargetsChanged -= UpdateCountTargets;
        }

        private void UpdateCountTargets() => text.text = $"{targetsManager.ActivityTargetCount}/{targetsManager.ActivityTargets.Length}";
    }
}