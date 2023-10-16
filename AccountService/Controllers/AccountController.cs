using AccountService.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AccountService.Controllers;

/// <summary>
/// This controller implements a basic REST HTTP endpoint for querying information about accounts in a repository.
/// How would you go about changing this code to improve it?
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAccountRepository _accountRepository;
    private readonly ILogger<AccountController> _logger;
    public AccountController(IAccountRepository accountRepository, ILogger<AccountController> logger)
    {
         _accountRepository = accountRepository;
        _logger = logger;
    }

    // GET /api/account?id={id}
    // Authorization: Basic htrqSjG1ua4r28iqgfgWNA==

    [HttpGet]
    public async Task<ActionResult<Models.Account>> Get(int id)
    {
        try
        {
            var authorizationHeader = Request.Headers["Authorization"].ToString();
            var account = await _accountRepository.GetAccountAsync(authorizationHeader, id);
            var user = await _accountRepository.GetUserAsync(authorizationHeader, account.UserId);
            var addresses = await _accountRepository.GetAddressesAsync(authorizationHeader, account.UserId);

            var content = new Models.Account
            {
                Id = account.Id,
                AddressLines = new[] { addresses.ShippingAddress.Street, addresses.ShippingAddress.Town, addresses.BillingAddress.Country },
                Email = account.EmailAddress,
                Forename = user.FirstName,
                Surname = user.LastName
            };

            return Ok(content);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in AccountController.Get");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing the request.");
        }

    }
}
