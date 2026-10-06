using Microsoft.AspNetCore.Identity;

namespace CivicAction.Models;

public enum Grade
{
    Freshman, Sophomore, Junior, Senior
}

public class AppUser : IdentityUser
{
    public string Id { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;
    public string FirstMidName { get; set; } = string.Empty;

    public Grade? Grade { get; set; }
    public string? School { get; set; } = string.Empty;
    
    public bool IsAdmin { get; set; }

    public ICollection<Verification> AdminVerifications { get; set; } = new List<Verification>();
    public ICollection<Verification> StudentVerifications { get; set; } = new List<Verification>();
}