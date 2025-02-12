using Microsoft.AspNetCore.Identity;

namespace AppStore.Models.Domain;

public class ApplicationUser : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}
