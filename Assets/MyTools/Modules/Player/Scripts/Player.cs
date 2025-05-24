using UnityEngine.Events;

namespace MyTools.PlayerSystem
{
    public class Player
    {
        public UnityAction<int> MoneyChanged;

        public int Money { get; private set; }

        public Player()
        {
            Money = SaveManager.LoadMoney();
        }

        public void AddMoney(int money) 
        {
            Money += money;
            InvokeMoneyChanged();
        }

        private void InvokeMoneyChanged() 
        {
            MoneyChanged?.Invoke(Money);
            SaveManager.SaveMoney(Money);
        } 
    }
}