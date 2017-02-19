namespace LayerCQRS.CommandContext
{
    using Domain.Entities.CQRS;
    using Domain.Entities;

    public class ReturnBikeCommandContext : ICommandContext
    {
        public string BikeName { get; set; }
        public Employee Employee { get; set; }
    }
}
