using UnityEngine.Events;

namespace MyTools.PlayerSystem
{
    public class Player
    {
        public UnityAction<int> MoneyChanged;

        public int Money { get; private set; }

        public Player() { }

        public void Initialize() => Money = SaveManager.LoadMoney();

        public bool AddMoney(int money)
        {
            if (Money + money >= 0)
            {
                Money += money;
                InvokeMoneyChanged();
                return true;
            }
            return false;
        }

        private void InvokeMoneyChanged()
        {
            MoneyChanged?.Invoke(Money);
            SaveManager.SaveMoney(Money);
        }
    }
}