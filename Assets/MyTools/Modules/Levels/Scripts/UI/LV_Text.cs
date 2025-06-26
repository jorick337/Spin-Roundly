using UnityEngine;
using UnityEngine.UI;

namespace MyTools.Levels.UI
{
    public class LV_Text : MonoBehaviour
    {
        [SerializeField] private Text _text;

        // Managers
        private LevelsManager _levelsManager;

        private void Awake() => Initialize();

        private void Initialize() 
        {
            _levelsManager = LevelsManager.Instance;
            SetText(_levelsManager.ChosedNumberLevel.ToString());
        }

        private void SetText(string text) => _text.text = text;
    }
}