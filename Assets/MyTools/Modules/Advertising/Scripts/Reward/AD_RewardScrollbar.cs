using MyTools.UI.Animation;
using UnityEngine;
using UnityEngine.UI;

namespace MyTools.Advertising
{
    public class AD_RewardScrollbar : MonoBehaviour
    {
        [SerializeField] private AD_RewardButton _rewardButton;
        [SerializeField] private Scrollbar _scrollbar;
        [SerializeField] private ANIM_Scrollbar _animScrollbar;
        [SerializeField] private float[] _borderMultipliers = new float[3];
        [SerializeField] private float[] _multipliers = new float[3];
        [SerializeField] private int _initialReward = 0;
        [SerializeField] private bool _canInitialize = false;

        private int _currentReward = 0;

        private void Awake()
        {
            if (_canInitialize)
                Initialize();
        }

        private void OnEnable() => _scrollbar.onValueChanged.AddListener(SetCurrentReward);
        private void OnDisable() => _scrollbar.onValueChanged.RemoveListener(SetCurrentReward);

        public void Initialize() => EnableAnimationScrollbar();

        public void SetInitialReward(int reward) 
        {
            _initialReward = reward;
            _currentReward = reward;
            SetReward();
        } 

        private void SetReward() => _rewardButton.SetReward(_currentReward);
        private void EnableAnimationScrollbar() => _animScrollbar.enabled = true;
        
        private void SetCurrentReward(float progress)
        {
            for (int i = 0; i < _borderMultipliers.Length; i++)
            {
                if (progress < _borderMultipliers[i])
                {
                    _currentReward = (int)(_initialReward * _multipliers[i]);
                    SetReward();
                }
            }
        }
    }
}