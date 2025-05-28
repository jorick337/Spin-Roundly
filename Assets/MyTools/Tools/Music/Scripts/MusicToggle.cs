using Cysharp.Threading.Tasks;
using MyTools.UI;
using UnityEngine;
using UnityEngine.UI;

namespace MyTools.Music
{
    public class MusicToggle : MyButton
    {
        [Header("Only Image")]
        [SerializeField] private Image _image;
        [SerializeField] private Sprite _enableSoundSprite;
        [SerializeField] private Sprite _disableSoundSprite;

        [Header("Work with")]
        [SerializeField] private bool _canMusic = true;
        [SerializeField] private bool _canSounds = true;

        // Managers
        private MusicManager _musicManager;

        private async void Awake() 
        {
            _musicManager = MusicManager.Instance;
            await UniTask.WaitUntil(() => _musicManager.IsLoaded);
            Initialize();
        }

        public override void OnValidate()
        {
            if (_toggle == null)
                _toggle = GetComponent<Toggle>();

            if (_canMusic == false && _canSounds == false)
                _canMusic = true;

            Validate();
        }

        private void Initialize() 
        {
            UnregisterEvents();

            UpdateIsOn();
            UpdateSprite();

            RegisterEvents();
        }

        private void UpdateIsOn() 
        {
            if (_canMusic)
                _toggle.isOn = _musicManager.IsMusicActive;
            if (_canSounds)
                _toggle.isOn = _musicManager.IsSoundsActive;
        }

        private void UpdateSprite()
        {
            if (_image != null)
                _image.sprite = _toggle.isOn ? _enableSoundSprite : _disableSoundSprite;
        }

        private void UpdateMusic(bool isOn)
        {
            if (_canMusic)
                _musicManager.SetIsActiveMusic(isOn);
            if (_canSounds)
                _musicManager.SetIsActiveSounds(isOn);
        }

        public override void ClickToggle(bool isOn)
        {
            AnimateClick();
            PlayClickSound();
            UpdateMusic(isOn);
            UpdateSprite();
        }
    }
}