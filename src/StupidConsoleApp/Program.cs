namespace StupidConsoleApp
{
    using App;
    using Domain.Entities;
    using Domain.Repositories;
    using Domain.Services;

    public class Program
    {
        public static void Main(string[] args)
        {
            IRepository<Client> clientRepository = new Repository<Client>();
            IRepository<Employee> employeeRepository = new Repository<Employee>();
            IRepository<Bike> bikeRepository = new Repository<Bike>();
            IBikeNameVerifier bikeNameVerifier = new BikeNameVerifier(bikeRepository);
            IBikeService bikeService = new BikeService(bikeRepository, bikeNameVerifier);

            App app = new App(clientRepository, employeeRepository, bikeRepository, bikeService);

            app.AddBike("Кама", 50);
            app.AddBike("Кама", 100);
        }
    }
}
