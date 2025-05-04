using UnityEngine;

namespace Game.Target
{
    public class TargetFinder : MonoBehaviour
    {
        [Header("Core")]
        [SerializeField] private Transform transformToRotate;

        [Header("Managers")]
        [SerializeField] private TargetsManager targetsManager;

        public void FindAndDirectObjectTowardsTarget()
        {
            TargetObject targetObject = targetsManager.GetFirstNotAssembledItem();
            transformToRotate.LookAt(targetObject.transform);
        }
    }
}