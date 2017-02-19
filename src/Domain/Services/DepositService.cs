namespace Domain.Services
{
    using Domain.Entities.Deposits;
    using Domain.Entities;
    using System;


    public class DepositService : IDepositService
    {
        public void PutDeposit(Deposit deposit, RentPoint rentPoint)
        {
            if (deposit == null)
                throw new ArgumentNullException(nameof(deposit));

            switch (deposit.Type)
            {
                case DepositTypes.Money:
                    rentPoint.Cashbox.PutMoney(((MoneyDeposit)deposit).Sum);
                    break;

                case DepositTypes.Passport:
                    rentPoint.Safe.putPassport((PassportDeposit)deposit);
                    break;
            }
        }

        public void ReturnDeposit(Deposit deposit, RentPoint rentPoint)
        {
            switch (deposit.Type)
            {
                case DepositTypes.Money:
                    rentPoint.Cashbox.TakeMoney(((MoneyDeposit)deposit).Sum);
                    break;

                case DepositTypes.Passport:
                    rentPoint.Safe.returnPassport((PassportDeposit)deposit);
                    break;
            }
        }
    }
}
