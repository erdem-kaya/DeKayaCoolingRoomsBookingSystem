using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class UserProfileEntity
{
    [Key]
    [ForeignKey(nameof(ApplicationUser))]
    public string Id { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string JobTitle { get; set; } = null!;
    public string? ProfilePicture { get; set; }

    public virtual ApplicationUserEntity ApplicationUser { get; set; } = null!;

    public virtual ICollection<BookingEntity> Bookings { get; set; } = [];
}