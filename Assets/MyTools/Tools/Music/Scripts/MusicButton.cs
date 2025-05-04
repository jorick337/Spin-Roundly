using UnityEngine;
using UnityEngine.UI;

namespace Game.Music
{
    public class MusicButton : MonoBehaviour
    {
        [Header("Music")]
        [SerializeField] private Button button;
        [SerializeField] private Image image;

        [Header("Sprites")]
        [SerializeField] private Sprite enableSoundSprite;
        [SerializeField] private Sprite disableSoundSprite;

        // Managers
        private MusicManager _musicManager;

        private void Awake()
        {
            _musicManager = MusicManager.Instance;
            UpdateSprite();
        }

        private void OnEnable()
        {
            button.onClick.AddListener(ChangeActiveSoundsAndSpriteButton);
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(ChangeActiveSoundsAndSpriteButton);
        }
        
        private void UpdateSprite() => image.sprite = _musicManager.IsMusicActive ? enableSoundSprite : disableSoundSprite;

        private void ChangeActiveSoundsAndSpriteButton()
        {
            _musicManager.SwitchActiveMusic();
            UpdateSprite();
        }
    }
}