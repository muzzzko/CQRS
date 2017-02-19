namespace Domain.Entities
{
    using System;
    using Deposits;
    using System.Collections.Generic;
    using System.Linq;

    public class Cashbox : IEntity
    {
     
        public Cashbox(decimal money)
        {
            Money = money;
        }

        public decimal Money { get; protected set; }

        public void PutMoney(decimal money)
        {
            if (money < 0)
                throw new ArgumentOutOfRangeException(nameof(money));

            Money += money;
        }

        public void TakeMoney(decimal money)
        {
            if (money < 0)
                throw new ArgumentOutOfRangeException(nameof(money));

            if (money > Money)
                throw new InvalidOperationException("Not enough money");

            Money -= money;
        }
    }
}
