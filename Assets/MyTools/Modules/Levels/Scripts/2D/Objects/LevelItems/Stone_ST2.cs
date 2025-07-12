using MyTools.UI;
using UnityEngine;

namespace MyTools.Levels.TwoDimensional.Objects
{
    public class Stone_ST2 : LevelItem
    {
        [Header("Stone")]
        [SerializeField] private Teleport _teleport;

        protected override void Restart()
        {
            base.Restart();
            _teleport.SendToTarget();
        }
    }
}