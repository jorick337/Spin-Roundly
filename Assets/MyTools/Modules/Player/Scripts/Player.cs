using UnityEngine.Events;

namespace MyTools.PlayerSystem
{
    public class Player
    {
        public UnityAction<int> MoneyChanged;
        public UnityAction<int> TrophiesChanged;

        public int Money { get; private set; }
        public int Trophies { get; private set; }

        public Player()
        {
            Money = SaveManager.LoadMoney();
            Trophies = SaveManager.LoadTrophy();
        }

        public void AddMoney(int money)
        {
            Money += money;
            InvokeMoneyChanged();
        }

        public void AddTrophy(int trophy)
        {
            Trophies += trophy;
            InvokeTrophiesChanged();
        }

        private void InvokeMoneyChanged()
        {
            MoneyChanged?.Invoke(Money);
            SaveManager.SaveMoney(Money);
        }

        private void InvokeTrophiesChanged()
        {
            TrophiesChanged?.Invoke(Trophies);
            SaveManager.SaveTrophy(Trophies);
        }
    }
}