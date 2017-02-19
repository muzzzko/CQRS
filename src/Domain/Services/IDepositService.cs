namespace Domain.Services
{
    using Entities.Deposits;
    using Entities;

    public interface IDepositService
    {
        void PutDeposit(Deposit deposit,RentPoint rentPoint);

        void ReturnDeposit(Deposit deposit, RentPoint rentpoint);
    }
}
