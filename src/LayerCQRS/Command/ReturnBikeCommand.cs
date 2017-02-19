namespace LayerCQRS.Command
{
    using Domain.Entities.CQRS;
    using Domain.Services;
    using Domain.Repositories;
    using LayerCQRS.CommandContext;
    using System.Linq;
    using Domain.Entities;

    public class ReturnBikeCommand : ICommand<ReturnBikeCommandContext>
    {
        private readonly IRentService _rentService;
        private readonly IRepository<Bike> _bikeRepository;
        private readonly IRepository<RentPoint> _rentPointRepository;


        public ReturnBikeCommand(IRentService rentService,
            IRepository<Bike> bikeRepository,
            IRepository<RentPoint> RentPointRepository)
        {
            _rentService = rentService;
            _bikeRepository = bikeRepository;
            _rentPointRepository = RentPointRepository;
        }



        public void Execute(ReturnBikeCommandContext commandContext)
        {
            _rentService.Return(
                _bikeRepository.All().SingleOrDefault<Bike>(x => x.Name == commandContext.BikeName),
                _rentPointRepository.All().SingleOrDefault<RentPoint>(x => x.Employee.Equals(commandContext.Employee))
               );
        }
    }
}
