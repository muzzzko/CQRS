namespace Domain.Services
{
    public interface IBikeNameVerifier
    {
        bool IsFree(string name);
    }
}
