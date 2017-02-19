namespace LayerCQRS.CommandContext
{
    using Domain.Entities.CQRS;
    using Domain.Entities;

    public class AddNewRentPointCommandContext : ICommandContext
    {
        public Employee Employee { get; set; }
        public decimal Money { get; set; } 
    }
}
