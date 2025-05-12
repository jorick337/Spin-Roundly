using MyTools.Music;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Music
{
    public class MusicButton : MonoBehaviour
    {
        [Header("Music")]
        [SerializeField] private Toggle toggle;
        [SerializeField] private Image image;

        [Header("Sprites")]
        [SerializeField] private Sprite enableSoundSprite;
        [SerializeField] private Sprite disableSoundSprite;

        // Managers
        private MusicManager _musicManager;

        private void Awake()
        {
            _musicManager = MusicManager.Instance;
            toggle.isOn = _musicManager.IsMusicActive;
            UpdateSprite();
        }

        private void OnEnable() => toggle.onValueChanged.AddListener(UpdateActiveMusicAndSprite);
        private void OnDisable() => toggle.onValueChanged.RemoveListener(UpdateActiveMusicAndSprite);
        
        private void UpdateSprite() => image.sprite = toggle.isOn ? enableSoundSprite : disableSoundSprite;

        private void UpdateActiveMusicAndSprite(bool isActiveMusic)
        {
            _musicManager.SetIsActiveMusic(isActiveMusic);
            UpdateSprite();
        }
    }
}