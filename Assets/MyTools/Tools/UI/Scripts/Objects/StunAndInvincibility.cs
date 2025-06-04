using Cysharp.Threading.Tasks;
using UnityEngine;

namespace MyTools.UI.Objects
{
    public class StunAndInvincibility : MonoBehaviour
    {
        [SerializeField] private int _firstLayer;
        [SerializeField] private int _secondLayer;
        [SerializeField] private float _time;

        public bool IsFinished { get; private set; } = false;

        private bool _isActive = false;

        public async void Stun()
        {
            if (_isActive)
                return;

            _isActive = true;
            IsFinished = false;

            DisableCollision();
            await UniTask.WaitForSeconds(_time);
            EnableCollision();

            _isActive = false;
            IsFinished = true;
        }

        public async UniTask WaitUntilFinished() => await UniTask.WaitUntil(() => IsFinished == true);

        private void EnableCollision() => SetCollisionIgnored(false);
        private void DisableCollision() => SetCollisionIgnored(true);

        private void SetCollisionIgnored(bool ignore) => Physics2D.IgnoreLayerCollision(_firstLayer, _secondLayer, ignore);
    }
}