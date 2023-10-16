using AccountService.Constants;
using AccountService.Exceptions;

namespace AccountService.Repositories;

/// <summary>
/// A class representing a repository that can be queried for users and accounts.
/// This class is intended to be a stub implementation, so
/// do not concern yourself with its implementation for the purposes of the exercise.
/// </summary>
public class AccountRepository : IAccountRepository
{
    private static readonly TimeSpan StubDelay = TimeSpan.FromMilliseconds(250);
    private readonly IUserRepository _userRepository;
    private readonly IAddressRepository _addressRepository;
    private readonly IHttpContextAccessor _httpContext;
    public AccountRepository(IUserRepository userRepository, IAddressRepository addressRepository, IHttpContextAccessor httpContext)
    {
        _userRepository = userRepository;
        _addressRepository = addressRepository;
        _httpContext = httpContext;

    }

    public async Task<Account> GetAccountAsync(string authToken, int id)
    {
        if (!AuthTokens.Contains(authToken))
        {
            throw new UnauthorizedAccessException();
        }

        await Task.Delay(StubDelay);

        return Accounts.FirstOrDefault(a => a.Id == id);
    }

    public async Task<Models.Account> GetUserAccountAsync(string authToken, int id)
    {
        string authorizationHeader = GetAuthorizationHeader();
        var account = await GetAccountAsync(authorizationHeader, id);
        if (account == null)
        {
            throw new UserFriendlyException(ErrorMessages.ACCOUNT_NOT_FOUND);
        }
        var user = await _userRepository.GetUserAsync(authorizationHeader, account.UserId);
        var addresses = await _addressRepository.GetAddressesAsync(authorizationHeader, account.UserId);

        Models.Account content = MapToAccountModel(account, user, addresses);

        return content;
    }

    #region private fields

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

    private string GetAuthorizationHeader()
    {
        string authorizationHeader = _httpContext.HttpContext.Request.Headers["Authorization"].ToString();
        if (string.IsNullOrEmpty(authorizationHeader))
        {
            throw new UnauthorizedAccessException();
        }
        return authorizationHeader;
    }

    private Models.Account MapToAccountModel(Account account, User user, Addresses addresses)
    {
        return new Models.Account
        {
            Id = account.Id,
            AddressLines = new[]
            {
                    addresses.ShippingAddress.Street,
                    addresses.ShippingAddress.Town,
                    addresses.BillingAddress?.Country ?? string.Empty
                },
            Email = account.EmailAddress,
            Forename = user.FirstName,
            Surname = user.LastName
        };
    }

    #endregion
}
