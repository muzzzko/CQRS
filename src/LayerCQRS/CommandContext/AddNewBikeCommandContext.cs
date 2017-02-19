namespace LayerCQRS.CommandContext
{
    using Domain.Entities.CQRS;
    using Domain.Entities;

    public class AddNewBikeCommandContext : ICommandContext
    {
        public string Name { get; set; }

        public decimal HourCost { get; set; }
    }
}
