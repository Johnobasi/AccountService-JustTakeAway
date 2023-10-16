namespace AccountService.Repositories
{

    public class AddressRepository : IAddressRepository
    {
        private static readonly TimeSpan StubDelay = TimeSpan.FromMilliseconds(250);
        public async Task<Addresses> GetAddressesAsync(string authToken, int userId)
        {
            if (!AuthTokens.Contains(authToken))
            {
                throw new UnauthorizedAccessException();
            }

            await Task.Delay(StubDelay);

            return Addresses.FirstOrDefault(a => a.UserId == userId);
        }

        #region private fields
        private readonly Addresses[] Addresses = new[]
        {
            new Addresses() { UserId = 5, ShippingAddress = new Address() { Street = "742 Evergreen Terrace", Town = "Springfield", Country = "USA" } },
            new Addresses() { UserId = 6, ShippingAddress = new Address() { Street = "Springfield Power Plant", Town = "Springfield", Country = "USA" }, BillingAddress = new Address() { Street = "1000 Mammon Lane", Town = "Springfield", Country = "USA" } },
        };

        private readonly string[] AuthTokens = new[]
        {
            "Basic htrqSjG1ua4r28iqgfgWNA==",
            "Basic JCvRx2emAYGo3L2E3b5i2A=="
        };

        #endregion
    }


}
