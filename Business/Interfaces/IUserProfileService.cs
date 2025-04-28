using Business.Model;
using Domain.Models.UserProfileData;

namespace Business.Interfaces
{
    public interface IUserProfileService
    {
        Task<UserResult> AddToUserRoleAsync(string userId, string roleName);
        Task<UserResult> CreateUserProfileAsync(UserProfileRegistrationForm form, string roleName = "User");
        Task<UserResult> DeleteUserProfileAsync(string userId);
        Task<UserResult> GetAllUsersAsync(bool orderByDescending = false);
        Task<UserResult> GetUserProfileByEmailAsync(string email);
        Task<UserResult> GetUserProfileByIdAsync(string userId);
        Task<UserResult> UpdateUserProfileAsync(string userId, UserProfileUpdateForm updateFormModel);
    }
}