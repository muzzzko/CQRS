namespace Domain.Entities.CQRS
{
    public interface ICommandBuilder
    {
        void Execute<TCommandContext>(TCommandContext commandContext)
            where TCommandContext : ICommandContext;
    }
}
