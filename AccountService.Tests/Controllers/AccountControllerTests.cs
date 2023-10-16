using AccountService.Controllers;
using AccountService.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace AccountService.Tests.Controllers;

public class AccountControllerTests
{
    private readonly Mock<IAccountRepository> _mockAccountRepository;
    private readonly Mock<ILogger<AccountController>> _mockLogger;
    private readonly AccountController _accountController;
    public AccountControllerTests()
    {
         _mockAccountRepository = new Mock<IAccountRepository>();
        _mockLogger = new Mock<ILogger<AccountController>>(); 
        _accountController = new AccountController(_mockAccountRepository.Object, _mockLogger.Object);
        _accountController.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext()
        };

    }
    [Fact]
    public async Task AccountController_Get_ReturnsOkResultAsync()
    {

        // Arrange
        var id = 1;
        var authorizationHeader = "Basic htrqSjG1ua4r28iqgfgWNA==";
        var account = new Account { Id = 1, EmailAddress = "test@example.com" };
        var user = new User { FirstName = "John", LastName = "Doe" };
        var addresses = new Addresses { ShippingAddress = new Address { Street = "123 Main St", Town = "City", Country = "US" }};


        _accountController.HttpContext.Request.Headers["Authorization"] = authorizationHeader;

        _mockAccountRepository.Setup(x => x.GetAccountAsync(authorizationHeader, id)).ReturnsAsync(account);

        // Act
        var result = await _accountController.Get(id);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var model = Assert.IsType<Account>(okResult.Value);
        Assert.Equal(id, model.Id);
        Assert.Equal("fakeAccount@test.com", model.EmailAddress);
        Assert.Equal("John", model.UserId.ToString());
    }

    [Fact]
    public async Task AccountController_Get_ReturnsInternalServerError()
    {
        // Arrange
        var id = 1;
        var authorizationHeader = "Basic htrqSjG1ua4r28iqgfgWNA==";
        _accountController.HttpContext.Request.Headers["Authorization"] = authorizationHeader;
        _mockAccountRepository.Setup(repo => repo.GetAccountAsync(authorizationHeader, id)).ThrowsAsync(new Exception("An error occurred while processing the request"));


        // Act
        var result = await _accountController.Get(id);

        // Assert
        var statusCodeResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
    }
}

