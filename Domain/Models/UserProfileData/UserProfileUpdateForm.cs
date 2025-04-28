namespace Domain.Models.UserProfileData;

public class UserProfileUpdateForm
{
    public string Id { get; set; } = null!;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? JobTitle { get; set; }
    public string? ProfilePicture { get; set; }
}
