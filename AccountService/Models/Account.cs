namespace AccountService.Models;

public class Account
{
    public int Id { get; set; }
    public string Forename { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public IList<string> AddressLines { get; set; }
}
