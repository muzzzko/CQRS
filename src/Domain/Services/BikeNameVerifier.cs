namespace Domain.Services
{
    using System.Linq;
    using Entities;
    using Repositories;

    public class BikeNameVerifier : IBikeNameVerifier
    {
        private readonly IRepository<Bike> _repository;



        public BikeNameVerifier(IRepository<Bike> repository)
        {
            _repository = repository;
        }



        public bool IsFree(string name)
        {
            return _repository.All().All(x => x.Name != name);
        }
    }
}
