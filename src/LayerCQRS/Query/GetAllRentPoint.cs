namespace LayerCQRS.Query
{
    using Domain.Entities.CQRS;
    using LayerCQRS.Criterion;
    using System.Collections.Generic;
    using Domain.Entities;
    using Domain.Repositories;

    public class GetAllRentPoint : IQuery<GetAllEntitiesCriterion,IEnumerable<RentPoint>>
    {
        private readonly IRepository<RentPoint> _rentPointRepository;



        public GetAllRentPoint(IRepository<RentPoint> rentPointRepository)
        {
            _rentPointRepository = rentPointRepository;
        }



        public IEnumerable<RentPoint> Ask(GetAllEntitiesCriterion criterion)
        {
            return _rentPointRepository.All();
        }

    }
}
