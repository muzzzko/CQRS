namespace Domain.Services
{
    using Entities;

    public interface IBikeService
    {
        void AddBikeToRentPoint(string bikeName, Employee employee);

        void AddBike(string name, decimal hourCost);

        void Rename(Bike bike, string name);

        void Remove(Bike bike);

        void MakeReservation(string bikeName, Client client);
    }
}
