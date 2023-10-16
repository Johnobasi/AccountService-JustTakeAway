namespace AccountService.Repositories;

/// <summary>
/// A class representing a repository that can be queried for users and accounts.
/// This class is intended to be a stub implementation, so
/// do not concern yourself with its implementation for the purposes of the exercise.
/// </summary>
public class AccountRepository : IAccountRepository
{
    private static readonly TimeSpan StubDelay = TimeSpan.FromMilliseconds(250);

    public async Task<Account> GetAccountAsync(string authToken, int id)
    {
        if (!AuthTokens.Contains(authToken))
        {
            throw new UnauthorizedAccessException();
        }

        await Task.Delay(StubDelay);

        return Accounts.FirstOrDefault(a => a.Id == id);
    }

    public async Task<User> GetUserAsync(string authToken, int userId)
    {
        if (!AuthTokens.Contains(authToken))
        {
            throw new UnauthorizedAccessException();
        }

        await Task.Delay(StubDelay);

        return Users.FirstOrDefault(u => u.Id == userId);
    }

    public async Task<Addresses> GetAddressesAsync(string authToken, int userId)
    {
        if (!AuthTokens.Contains(authToken))
        {
            throw new UnauthorizedAccessException();
        }

        await Task.Delay(StubDelay);

        return Addresses.FirstOrDefault(a => a.UserId == userId);
    }

    private readonly string[] AuthTokens = new[]
    {
        "Basic htrqSjG1ua4r28iqgfgWNA==",
        "Basic JCvRx2emAYGo3L2E3b5i2A=="
    };

    private readonly Account[] Accounts = new[]
    {
        new Account() { Id = 1, UserId = 4, EmailAddress = "bart.simpson@doh.net" },
        new Account() { Id = 2, UserId = 5, EmailAddress = "homer.simpson@doh.net" },
        new Account() { Id = 3, UserId = 6, EmailAddress = "charles_m_burns@fission.com" }
    };

    private readonly User[] Users = new[]
    {
        new User() { Id = 4, FirstName = "Bart", LastName = "Simpson", Age = 10 },
        new User() { Id = 5, FirstName = "Homer", LastName = "Simpson", Age = 34 },
        new User() { Id = 6, FirstName = "Charles", LastName = "Burns", Age = 81 },
    };

    private readonly Addresses[] Addresses = new[]
    {
        new Addresses() { UserId = 5, ShippingAddress = new Address() { Street = "742 Evergreen Terrace", Town = "Springfield", Country = "USA" } },
        new Addresses() { UserId = 6, ShippingAddress = new Address() { Street = "Springfield Power Plant", Town = "Springfield", Country = "USA" }, BillingAddress = new Address() { Street = "1000 Mammon Lane", Town = "Springfield", Country = "USA" } },
    };
}
