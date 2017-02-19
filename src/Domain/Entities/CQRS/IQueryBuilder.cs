namespace Domain.Entities.CQRS
{
    public interface IQueryBuilder
    {
        IQueryFor<TResult> For<TResult>();
    }
}