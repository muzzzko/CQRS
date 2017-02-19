namespace Domain.Services
{
    using Entities;

    public interface IDepositCalculator
    {
        decimal Calculate(Bike bike);
    }
}