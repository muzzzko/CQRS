namespace LayerCQRS.Query
{
    using LayerCQRS.Criterion;
    using Domain.Entities.CQRS;
    using System.Collections.Generic;
    using Domain.Entities;
    using Domain.Repositories;

    public class GetAllBikes : IQuery<GetAllEntitiesCriterion,IEnumerable<Bike>>
    {
        private readonly IRepository<Bike> _bikeRepository;



        public GetAllBikes(IRepository<Bike> bikeRepository)
        {
            _bikeRepository = bikeRepository;
        }



        public IEnumerable<Bike> Ask(GetAllEntitiesCriterion criterion)
        {
            return _bikeRepository.All();
        }
    }
}
