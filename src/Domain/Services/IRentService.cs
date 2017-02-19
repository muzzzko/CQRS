namespace Domain.Services
{
    using Entities;
    using Entities.Deposits;

    public interface IRentService
    {
        void Take(Client client, Bike bike, Deposit deposit);

        void Return(Bike bike, RentPoint rentPoint);
    }
}