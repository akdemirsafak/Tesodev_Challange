using Microsoft.AspNetCore.Identity;

namespace Customer.Domain.Entity;

public class ApiUser : IdentityUser
{
    public string Name { get; set; }
    public virtual Address? Address { get; set; }
}
