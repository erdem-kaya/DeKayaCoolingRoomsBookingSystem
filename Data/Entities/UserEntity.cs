using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class ApplicationUser : IdentityUser
{
    [Required]
    public string FirstName { get; set; } = null!;
    
    [Required]
    public string LastName { get; set; } = null!;

    [MaxLength(100)]
    public string? JobTitle { get; set; } 

    public virtual ICollection<Booking> Bookings { get; set; } = [];
}
