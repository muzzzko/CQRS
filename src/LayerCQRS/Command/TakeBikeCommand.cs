namespace LayerCQRS.Command
{
    using LayerCQRS.CommandContext;
    using Domain.Services;
    using Domain.Entities.CQRS;
    using Domain.Repositories;
    using Domain.Entities;
    using System.Linq;

    public class TakeBikeCommand : ICommand<TakeBikeCommandContext>
    {
        private readonly IRentService _rentService;
        private readonly IRepository<Bike> _bikeRepository;


        public TakeBikeCommand(IRentService rentService,
            IRepository<Bike> bikeRepository)
        {
            _rentService = rentService;
            _bikeRepository = bikeRepository;
        }



        public void Execute(TakeBikeCommandContext commandContext)
        {
            _rentService.Take(
                commandContext.Client,
                _bikeRepository.All().SingleOrDefault<Bike>(x => x.Name == commandContext.BikeName),
                commandContext.Deposit);
        }
    }
}
