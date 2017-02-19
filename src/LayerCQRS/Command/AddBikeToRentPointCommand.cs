namespace LayerCQRS.Command
{
    using LayerCQRS.CommandContext;
    using Domain.Entities.CQRS;
    using Domain.Services;

    public class AddBikeToRentPointCommand : ICommand<AddBikeToRentPointCommandContext>
    {
        private readonly IBikeService  _bikeService;



        public AddBikeToRentPointCommand(IBikeService bikeService)
        {
            _bikeService = bikeService;
        }



        public void Execute(AddBikeToRentPointCommandContext commandContext)
        {
            _bikeService.AddBikeToRentPoint(
                commandContext.BikeName,
                commandContext.Employee);
        }
    }
}
