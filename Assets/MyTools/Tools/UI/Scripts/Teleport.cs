using UnityEngine;

namespace MyTools.UI
{
    public class Teleport : MonoBehaviour
    {
        [SerializeField] private Transform _targetTrans;
        [SerializeField] private Transform _objTrans;

        private void SendToTarget() => _objTrans.position = _targetTrans.position;
    }
}