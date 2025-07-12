using Cysharp.Threading.Tasks;
using UnityEngine;

namespace MyTools.UI.Objects
{
    public class CollusionIgnore : MonoBehaviour
    {
        [SerializeField] private LayerMask _firstLayer;
        [SerializeField] private LayerMask _secondLayer;
        [SerializeField] private float _time;

        public bool IsFinished { get; private set; } = false;

        private bool _isActive = false;

        public async void Apply()
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

        public void EnableCollision() => SetCollisionIgnored(false);
        public void DisableCollision() => SetCollisionIgnored(true);

        private void SetCollisionIgnored(bool ignore)
        {
            for (int i = 0; i < 32; i++)
            {
                if ((_firstLayer.value & (1 << i)) == 0) continue;

                for (int j = 0; j < 32; j++)
                {
                    if ((_secondLayer.value & (1 << j)) == 0) continue;

                    Physics2D.IgnoreLayerCollision(i, j, ignore);
                }
            }
        }
    }
}