namespace AccountService.Repositories;

public interface IAccountRepository
{
    Task<Account> GetAccountAsync(string authToken, int id);
    Task<Addresses> GetAddressesAsync(string authToken, int userId);
    Task<User> GetUserAsync(string authToken, int userId);
}
