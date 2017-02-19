namespace LayerCQRS.CommandContext
{
    using Domain.Entities.CQRS;
    using Domain.Entities;
    using Domain.Entities.Deposits;

    public class TakeBikeCommandContext : ICommandContext
    {
        public string BikeName { get; set; }
        public Client Client { get; set; }
        public Deposit Deposit { get; set; }
    }
}
