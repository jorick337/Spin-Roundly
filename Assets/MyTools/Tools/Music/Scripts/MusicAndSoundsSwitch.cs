using UnityEngine;
using UnityEngine.UI;

namespace Game.Music
{
    public class MusicAndSoundsSwitch : MonoBehaviour
    {
        [Header("Core")]
        [SerializeField] private Button musicButton;
        [SerializeField] private Button soundsButton;

        // Managers
        private MusicManager _musicManager;

        private void Awake()
        {
            _musicManager = MusicManager.Instance;
        }

        private void OnEnable()
        {
            musicButton.onClick.AddListener(SwitchActiveMusic);
            soundsButton.onClick.AddListener(SwitchActiveSounds);
        }

        private void OnDisable()
        {
            musicButton.onClick.RemoveListener(SwitchActiveMusic);
            soundsButton.onClick.RemoveListener(SwitchActiveSounds);
        }

        private void SwitchActiveMusic() =>_musicManager.SwitchActiveMusic();
        private void SwitchActiveSounds() => _musicManager.SwitchActiveSounds();
    }
}