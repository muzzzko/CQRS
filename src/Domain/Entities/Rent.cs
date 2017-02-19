namespace Domain.Entities
{
    using System;
    using Deposits;

    public class Rent : IEntity
    {
        protected internal Rent(Client client, Bike bike, Deposit deposit)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            if (bike == null)
                throw new ArgumentNullException(nameof(bike));

            if (deposit == null)
                throw new ArgumentNullException(nameof(deposit));

            StartedAt = DateTime.UtcNow;
            StartRentPoint = bike.RentPoint;
            Client = client;
            Bike = bike;
            Deposit = deposit;
            HourCost = bike.HourCost;
            Bike.Take();
            bike.UnLockOnRentPoint();
            StartRentPoint.RemoveBike(bike);

        }



        public readonly RentPoint StartRentPoint;

        public readonly DateTime StartedAt;

        public RentPoint EndRentPoint { get; protected set; }
  
        public readonly Client Client;

        public readonly Bike Bike;

        public readonly Deposit Deposit;

        public readonly decimal HourCost;

        protected internal void End(RentPoint rentPoint,decimal sum)
        {
            if (rentPoint == null)
                throw new ArgumentNullException(nameof(rentPoint));

            EndRentPoint = rentPoint;
            EndRentPoint.Cashbox.PutMoney(sum);
            Bike.Return();
            Bike.LockOnRentPoint(EndRentPoint);
            Bike.MoveTo(EndRentPoint);
        }
    }
}
