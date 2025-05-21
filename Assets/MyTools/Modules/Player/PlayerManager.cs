using UnityEngine;

namespace MyTools.PlayerSystem
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager Instance { get; private set; }

        public Player Player { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                Player = new();
            }
            else
                Destroy(gameObject);
        }
    }
}