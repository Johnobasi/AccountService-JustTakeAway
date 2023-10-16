namespace AccountService.Repositories;

public class Addresses
{
    public int UserId { get; set; }
    public Address BillingAddress { get; set; }
    public Address ShippingAddress { get; set; }
}
