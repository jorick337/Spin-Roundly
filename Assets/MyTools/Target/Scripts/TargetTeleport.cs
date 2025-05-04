using UnityEngine;

namespace Game.Target
{
    public class TargetTeleport : MonoBehaviour
    {
        [Header("Core")]
        [SerializeField] private GameObject teleportObject;
        [SerializeField] private Vector3 coordinats;

        public void Teleport() => teleportObject.transform.localPosition = coordinats;
    }
}