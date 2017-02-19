namespace Domain.Services
{
    using System;
    using System.Linq;
    using Entities;
    using Repositories;

    public class BikeService : IBikeService
    {
        private readonly IRepository<Bike> _bikeRepository;
        private readonly IBikeNameVerifier _bikeNameVerifier;
        private readonly IRepository<RentPoint> _rentPointRepository;

        
        public BikeService(IRepository<Bike> bikeRepository,
            IRepository<RentPoint> rentPointRepository,
            IBikeNameVerifier bikeNameVerifier
           )
        {
            if (bikeRepository == null)
                throw new ArgumentNullException(nameof(bikeRepository));

            if (bikeNameVerifier == null)
                throw new ArgumentNullException(nameof(bikeNameVerifier));

            if (rentPointRepository == null)
                throw new ArgumentNullException(nameof(rentPointRepository));

            _bikeRepository = bikeRepository;
            _bikeNameVerifier = bikeNameVerifier;
            _rentPointRepository = rentPointRepository;
        }



        public void AddBike(string name, decimal hourCost)
        {
            if (!_bikeNameVerifier.IsFree(name))
                throw new InvalidOperationException("Bike with same name already exists");

            Bike newBike = new Bike(name, hourCost);

            _bikeRepository.Add(newBike);
        }

        public void AddBikeToRentPoint(string bikeName, Employee employee)
        {
            if (_bikeNameVerifier.IsFree(bikeName))
                throw new InvalidOperationException("Bike doesn't exist");

            Bike bike = _bikeRepository.All().SingleOrDefault<Bike>(x => x.Name == bikeName);

            RentPoint rentPoint = _rentPointRepository.All().SingleOrDefault<RentPoint>(x => x.Employee.Equals(employee));

            if (!rentPoint.Bikes.All<Bike>(x => x != bike))
                throw new InvalidOperationException("Bike with same name already exists");

            bike.LockOnRentPoint(rentPoint);
            rentPoint.AddBike(bike);
        }

        public void Rename(Bike bike, string name)
        {
            if (bike == null)
                throw new ArgumentNullException(nameof(bike));

            if (bike.Name == name)
                return;

            if (!_bikeNameVerifier.IsFree(name))
                throw new InvalidOperationException("Bike with same name already exists");

            bike.Rename(name);
        }

        public void Remove(Bike bike)
        {
            if (bike == null)
                throw new ArgumentNullException(nameof(bike));

            if (_bikeNameVerifier.IsFree(bike.Name))
                throw new InvalidOperationException("There is not this bike");

            _bikeRepository.Remove(bike);
        }

        public void MakeReservation(string bikeName,Client client)
        {
            if (bikeName == null)
                throw new ArgumentNullException(nameof(bikeName));

            if (_bikeNameVerifier.IsFree(bikeName))
                throw new InvalidOperationException("There is not this bike");

            Bike bike = _bikeRepository.All().SingleOrDefault(x => x.Name == bikeName);

            if (!bike.IsFree)
                throw new InvalidOperationException("Bike is not free");

            bike.ReservationEndedAt = DateTime.UtcNow.AddHours(2);
            bike.ClientMekedReservation = client;
        }
    }
}
