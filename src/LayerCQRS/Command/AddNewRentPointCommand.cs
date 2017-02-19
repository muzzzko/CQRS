namespace LayerCQRS.Command
{
    using LayerCQRS.CommandContext;
    using Domain.Entities.CQRS;
    using Domain.Repositories;
    using Domain.Entities;

    public class AddNewRentPointCommand : ICommand<AddNewRentPointCommandContext>
    {
        private readonly IRepository<RentPoint> _rentRepository;



        public AddNewRentPointCommand(IRepository<RentPoint> rentRepository)
        {
            _rentRepository = rentRepository;
        }



        public void Execute(AddNewRentPointCommandContext commandContext)
        {
            _rentRepository.Add(new RentPoint(commandContext.Employee, commandContext.Money));
        }
    }
}
