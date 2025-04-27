using Microsoft.AspNetCore.Identity;

namespace Data.Entities;

public class ApplicationUserEntity : IdentityUser
{
    public virtual UserProfileEntity? UsersProfile { get; set; }
}
