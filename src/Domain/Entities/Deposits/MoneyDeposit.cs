namespace Domain.Entities.Deposits
{
    using System;

    public class MoneyDeposit : Deposit
    {
        public MoneyDeposit(decimal sum) 
            : base(DepositTypes.Money)
        {
            if (sum < 0)
                throw new ArgumentOutOfRangeException(nameof(sum));

            Sum = sum;
        }


        public readonly decimal Sum;
    }
}
