using MyTools.UI.Animation;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MyTools.UI.Objects.Buttons
{
    public class AnimatedHoldButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private AnimationScale _animationScale;
        [SerializeField] private AudioSource _audioSource;

        private bool _isInside = false;
        private bool _isDown = false;

        public void OnPointerDown(PointerEventData eventData)
        {
            _isInside = true;
            _isDown = true;

            PlayClickSound();
            _animationScale?.AnimateOut();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _isDown = false;

            if (_isInside)
                _animationScale?.AnimateIn();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _isInside = true;

            if (_isDown)
                _animationScale?.AnimateOut();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _isInside = false;

            if (_isDown)
                _animationScale?.AnimateIn();
        }

        private void PlayClickSound() => _audioSource?.Play();
    }
}