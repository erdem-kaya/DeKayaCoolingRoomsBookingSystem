using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class ApplicationUserEntity : IdentityUser
{
    [Required]
    public string FirstName { get; set; } = null!;
    
    [Required]
    public string LastName { get; set; } = null!;

    [MaxLength(100)]
    public string? JobTitle { get; set; } 

    public virtual ICollection<BookingEntity> Bookings { get; set; } = [];
}
