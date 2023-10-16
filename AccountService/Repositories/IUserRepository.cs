namespace AccountService.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserAsync(string authToken, int userId);
    }
}
