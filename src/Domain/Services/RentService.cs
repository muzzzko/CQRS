namespace Domain.Services
{
    using System;
    using System.Linq;
    using Entities;
    using Entities.Deposits;
    using Repositories;

    public class RentService : IRentService
    {
        private readonly IDepositCalculator _depositCalculator;
        private readonly IRepository<Rent> _rentRepository;
        private readonly IDepositService _depositService;
        private readonly IBikeService _bikeService;


        public RentService(
            IDepositCalculator depositCalculator, 
            IRepository<Rent> rentRepository,
            IDepositService depositService,
            IBikeService bikeService)
        {
            if (depositCalculator == null)
                throw new ArgumentNullException(nameof(depositCalculator));

            if (rentRepository == null)
                throw new ArgumentNullException(nameof(rentRepository));

            _depositCalculator = depositCalculator;
            _rentRepository = rentRepository;
            _depositService = depositService;
            _bikeService = bikeService;
        }

        public DateTime? EndedAt { get; protected set; }

        public bool IsEnded => EndedAt.HasValue;

        public decimal Sum(Rent rent, DateTime StartAt)
        {
            if (IsEnded)
                throw new InvalidOperationException("Rent is already ended");
            EndedAt = DateTime.UtcNow;

            double totalHouts = Math.Ceiling((EndedAt.Value - rent.StartedAt).TotalHours);

            decimal sum;

            if (Math.Ceiling((EndedAt.Value - rent.StartedAt).TotalHours) <= 3)
                sum = (decimal)Math.Round(totalHouts * (double)rent.HourCost, 2);
            else sum = (decimal)Math.Round((totalHouts - 3) * (double)rent.HourCost * 0.8 + 3 * (double)rent.HourCost);

            EndedAt = null;

            return sum; 
        }

        public void Take(Client client, Bike bike, Deposit deposit)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            if (bike == null)
                throw new ArgumentNullException(nameof(bike));

            if (deposit == null)
                throw new ArgumentNullException(nameof(deposit));

            if (bike.RentPoint == null)
                throw new InvalidOperationException("Bike is not on rent point");

            if (!bike.IsFree)
                throw new InvalidOperationException("Bike is not free");

            if (bike.ReservationEndedAt.CompareTo(DateTime.UtcNow) > 0 && !bike.ClientMekedReservation.Equals(client)) 
                throw new InvalidOperationException("Bike was maked a resevation");

            bike.ClientMekedReservation = null;
            bike.ReservationEndedAt = DateTime.UtcNow;

            if (deposit.Type == DepositTypes.Money)
            {
                decimal depositSum = _depositCalculator.Calculate(bike);

                if (((MoneyDeposit)deposit).Sum < depositSum)
                    throw new InvalidOperationException("Deposit sum is not enough");
            }

            _depositService.PutDeposit(deposit,bike.RentPoint);
            
            Rent rent = new Rent(client, bike, deposit);

            _rentRepository.Add(rent);
        }

        public void Return(Bike bike, RentPoint rentPoint)
        {
            if (bike == null)
                throw new ArgumentNullException(nameof(bike));

            if (rentPoint == null)
                throw new ArgumentNullException(nameof(rentPoint));

            Rent rent = _rentRepository
                .All()
                .SingleOrDefault(
                    x => x.Bike == bike);

            if (rent == null)
                throw new InvalidOperationException("Rent not found");

            decimal sum = Sum(rent, rent.StartedAt);

            if (bike.Crushed)
            {
                rentPoint.Cashbox.PutMoney(sum);
                if (rent.Deposit.Type == DepositTypes.Passport)
                    _depositService.ReturnDeposit(rent.Deposit, rentPoint);
                _bikeService.Remove(bike);
            }
            else
            {
                _depositService.ReturnDeposit(rent.Deposit, rentPoint);
                rent.End(rentPoint, sum);
            }
            _rentRepository.Remove(rent);
        }
    }
}