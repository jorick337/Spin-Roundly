using System;
using System.Linq;
using UnityEngine;

namespace Game.Target
{
    public class TargetsManager : MonoBehaviour
    {
        #region EVENTS

        public Action<int> ActivityTargetChanged;
        public Action CountTargetsChanged;
        public Action TargetsCollected;

        #endregion

        #region CORE

        public static TargetsManager Instance { get; private set; }

        [Header("Core")]
        [SerializeField] private TargetObject[] targetObjects;

        public bool[] ActivityTargets { get; private set; }
        public int ActivityTargetCount { get; private set; }

        #endregion

        #region MONO

        private void Awake()
        {
            Instance = this;
        }

        #endregion

        #region INITIALIZATION

        public void Initialize(bool[] targets)
        {
            ActivityTargets = targets;
            ActivityTargetCount = ActivityTargets.Count(item => item);

            for (int i = 0; i < targets.Length; i++)
            {
                targetObjects[i].Initialize(ActivityTargets[i]);
            }

            InvokeCountTargetsChanged();
        }

        #endregion

        #region CORE LOGIC

        public void MarkAsFound(int index)
        {
            ActivityTargets[index] = true;
            ActivityTargetCount += 1;

            InvokeActivityTargetChanged(index);
            InvokeCountTargetsChanged();

            if (ActivityTargetCount == ActivityTargets.Length)
            {
                InvokeItemsCollected();
            }
        }

        #endregion

        #region VALUES

        public TargetObject GetFirstNotAssembledItem()
        {
            for (int i = 0; i < ActivityTargets.Length; i++)
            {
                if (!ActivityTargets[i])
                {
                    return targetObjects[i];
                }
            }

            return null;
        }

        #endregion

        #region CALLBACKS

        private void InvokeActivityTargetChanged(int index) => ActivityTargetChanged?.Invoke(index);
        private void InvokeCountTargetsChanged() => CountTargetsChanged?.Invoke();
        private void InvokeItemsCollected() => TargetsCollected?.Invoke();

        #endregion
    }
}