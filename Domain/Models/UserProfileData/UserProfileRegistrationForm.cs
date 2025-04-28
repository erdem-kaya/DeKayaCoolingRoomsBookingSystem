namespace Domain.Models.UserProfileData;

public class UserProfileRegistrationForm
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string JobTitle { get; set; } = null!;
    public string? ProfilePicture { get; set; }
}
