namespace Domain.Entities
{
    using System;

    public class Bike : IEntity
    {
        protected internal Bike(string name, decimal hourCost)
        {
            Rename(name);
            ChangeHourCost(hourCost);
            IsFree = true;
            ReservationEndedAt = DateTime.UtcNow;
            ClientMekedReservation = null;
        }



        public string Name { get; protected set; }

        public decimal HourCost { get; protected set; }

        public bool IsFree { get; protected set; }

        public RentPoint RentPoint { get; protected internal set; }

        public bool Crushed { get; protected internal set; }

        public DateTime ReservationEndedAt { get; protected internal set; }

        public Client ClientMekedReservation { get; protected internal set; }

        protected internal void LockOnRentPoint(RentPoint rentPoint)
        {
            if (rentPoint == null)
                throw new ArgumentNullException(nameof(rentPoint));
             
            RentPoint = rentPoint;
        }

        protected internal void UnLockOnRentPoint()
        {
            RentPoint = null;
        }
        
         

        protected internal void Rename(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            Name = name;
        }

        public void ChangeHourCost(decimal hourCost)
        {
            if (hourCost <= 0)
                throw new ArgumentOutOfRangeException(nameof(hourCost), "Hour cost must be more than 0");

            HourCost = hourCost;
        }

        public void MoveTo(RentPoint rentPoint)
        {
            if (!IsFree)
                throw new InvalidOperationException("Bike is not free");

            if (rentPoint == null)
                throw new ArgumentNullException(nameof(rentPoint));

            if (rentPoint == RentPoint)
            {
                this.LockOnRentPoint(rentPoint);
                rentPoint.AddBike(this);
                return;
            }
            RentPoint?.RemoveBike(this);

            rentPoint.AddBike(this);

            RentPoint = rentPoint;
        }



        protected internal void Take()
        {
            if (!IsFree)
                throw new InvalidOperationException("Bike is not free");

            IsFree = false;
        }

        protected internal void Return()
        {
            if (IsFree)
                throw new InvalidOperationException("Bike is free");

            IsFree = true;
        }
    }
}
