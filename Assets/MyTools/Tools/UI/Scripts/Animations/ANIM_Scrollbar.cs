using UnityEngine;
using UnityEngine.UI;

namespace MyTools.UI.Animation
{
    public class ANIM_Scrollbar : MonoBehaviour
    {
        [SerializeField] private Scrollbar scrollbar;
        [SerializeField] private float _speed = 0;
        [SerializeField] private bool _isRightDirection = true;

        private void Update() => Apply();

        private void Apply()
        {
            float value = scrollbar.value;
            float delta = _speed * Time.deltaTime;
            
            value += _isRightDirection ? delta : -delta;

            if (value >= 1)
            {
                value = 1f;
                _isRightDirection = false;
            }
            else if (value <= 0)
            {
                value = 0f;
                _isRightDirection = true;
            }

            scrollbar.value = value;
        }
    }
}