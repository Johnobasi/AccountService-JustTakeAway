namespace AccountService.Repositories
{
    public class UserRepository : IUserRepository
    {
        private static readonly TimeSpan StubDelay = TimeSpan.FromMilliseconds(250);
        public async Task<User> GetUserAsync(string authToken, int userId)
        {
            if (!AuthTokens.Contains(authToken))
            {
                throw new UnauthorizedAccessException();
            }

            await Task.Delay(StubDelay);

            return Users.FirstOrDefault(u => u.Id == userId);
        }
        #region private fields
        private readonly User[] Users = new[]
        {
            new User() { Id = 4, FirstName = "Bart", LastName = "Simpson", Age = 10 },
            new User() { Id = 5, FirstName = "Homer", LastName = "Simpson", Age = 34 },
            new User() { Id = 6, FirstName = "Charles", LastName = "Burns", Age = 81 },
        };

        private readonly string[] AuthTokens = new[]
        {
            "Basic htrqSjG1ua4r28iqgfgWNA==",
            "Basic JCvRx2emAYGo3L2E3b5i2A=="
        };
        #endregion
    }
}
