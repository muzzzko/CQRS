namespace Domain.Entities.Deposits
{
    public abstract class Deposit
    {
        protected Deposit(DepositTypes type)
        {
            Type = type;
        }



        public readonly DepositTypes Type;
    }
}
