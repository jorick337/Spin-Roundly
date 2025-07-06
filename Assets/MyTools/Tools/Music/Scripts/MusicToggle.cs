using MyTools.UI.Objects.Toggles;
using UnityEngine;
using UnityEngine.UI;

namespace MyTools.Music.UI
{
    public class MusicToggle : BaseToggle
    {
        [Header("Music")]
        [SerializeField] private Image _image;
        [SerializeField] private Sprite _enableSoundSprite;
        [SerializeField] private Sprite _disableSoundSprite;
        [SerializeField] private bool _canChangeActiveMusic = true;
        [SerializeField] private bool _canChangeActiveSounds = true;

        // Managers
        private MusicManager _musicManager;

        private async void Awake()
        {
            _musicManager = MusicManager.Instance;
            await _musicManager.WaitUntilLoaded();
            Initialize();
        }

        protected override void OnTogglePressed(bool isOn)
        {
            UpdateMusic(isOn);
            UpdateSprite();
        }

        private void Initialize()
        {
            UpdateIsOn();
            UpdateSprite();
        }

        private void UpdateIsOn()
        {
            if (_canChangeActiveMusic)
                _toggle.isOn = _musicManager.IsMusicActive;
            if (_canChangeActiveSounds)
                _toggle.isOn = _musicManager.IsSoundsActive;
        }

        private void UpdateSprite()
        {
            if (_image != null)
                _image.sprite = _toggle.isOn ? _enableSoundSprite : _disableSoundSprite;
        }

        private void UpdateMusic(bool isOn)
        {
            if (_canChangeActiveMusic)
                _musicManager.SetIsActiveMusic(isOn);
            if (_canChangeActiveSounds)
                _musicManager.SetIsActiveSounds(isOn);
        }
    }
}