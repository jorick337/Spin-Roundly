using UnityEngine.Events;

namespace MyTools.PlayerSystem
{
    public class Player
    {
        public UnityAction MoneyChanged;

        public int Money { get; private set; }

        public Player()
        {
            Money = SaveManager.LoadMoney();
        }

        public void AddMoney(int money) 
        {
            InvokeMoneyChanged();
            Money += money;
        }

        private void InvokeMoneyChanged() => MoneyChanged?.Invoke();
    }
}