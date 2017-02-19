namespace LayerCQRS.Query
{
    using LayerCQRS.Criterion;
    using Domain.Entities.CQRS;
    using System.Collections.Generic;
    using Domain.Entities;
    using Domain.Repositories;

    public class GetAllRents : IQuery<GetAllEntitiesCriterion, IEnumerable<Rent>>
    {
        private readonly IRepository<Rent> _rentRepository;



        public GetAllRents(IRepository<Rent> rentRepository)
        {
            _rentRepository = rentRepository;
        }



        public IEnumerable<Rent> Ask(GetAllEntitiesCriterion criterion)
        {
            return _rentRepository.All();
        }
    }
}
