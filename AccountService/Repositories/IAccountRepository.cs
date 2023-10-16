namespace AccountService.Repositories;

public interface IAccountRepository
{
    Task<Account> GetAccountAsync(string authToken, int id);
    Task<Models.Account> GetUserAccountAsync(string authToken, int id);
}
