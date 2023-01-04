namespace Store.Domain.Repositories
{
    public interface IDeliveryFeeRepository
    {
        decimal Get(string ZipCode);
    }
}
