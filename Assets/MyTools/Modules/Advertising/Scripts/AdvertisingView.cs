using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MyTools.Advertising
{
    public class AdvertisingView : MonoBehaviour
    {
        #region EVENTS

        public event UnityAction<bool> Finished;

        #endregion

        #region CORE

        [SerializeField] private float _time;
        [SerializeField] private Slider _slider;

        private float _currentTime;
        private bool _canGiveReward = true;

        // Managers
        private AdvertisingViewProvider _provider;

        #endregion

        #region MONO

        private void Start() => StartCoroutine(StartLoading());

        #endregion

        #region CORE LOGIC

        private IEnumerator StartLoading()
        {
            Restart();

            while (_currentTime < _time)
            {
                _slider.value = _currentTime / _time;
                _currentTime += Time.deltaTime;
                yield return null;
            }

            Finish();
        }

        private void Restart()
        {
            _currentTime = 0;
            _slider.value = 0;
        }

        private void Finish()
        {
            _slider.value = 1f;
            InvokeFinished();
            DestroySelf();
        }

        #endregion

        #region VALUES

        public void SetProvider(AdvertisingViewProvider provider) => _provider = provider;

        private async void DestroySelf() => await _provider.UnloadAsync();

        #endregion

        #region CALLBACKS

        private void InvokeFinished() => Finished?.Invoke(_canGiveReward);

        #endregion
    }
}