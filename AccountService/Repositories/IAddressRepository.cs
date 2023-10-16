namespace AccountService.Repositories
{
    public interface IAddressRepository
    {
        Task<Addresses> GetAddressesAsync(string authToken, int userId);
    }
}
