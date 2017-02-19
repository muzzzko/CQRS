namespace LayerCQRS.Command
{
    using Domain.Entities.CQRS;
    using CommandContext;
    using Domain.Services;
    using Domain.Entities;

    public class AddNewBikeCommand : ICommand<AddNewBikeCommandContext> 
    {
        private readonly IBikeService _bikeService;



        public AddNewBikeCommand(IBikeService bikeService)
        {
            _bikeService = bikeService;
        }


        public void Execute(AddNewBikeCommandContext commandContext)
        {
            _bikeService.AddBike(commandContext.Name, commandContext.HourCost);
        }
    }
}
