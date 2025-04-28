using Business.Factories;
using Business.Model;
using Data.Entities;
using Data.Interfaces;
using Domain.Extensions;
using Domain.Models.UserProfileData;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;

namespace Business.Services;

public class UserProfileService(IUserProfileRepository userProfileRepository, UserManager<ApplicationUserEntity> userManager, RoleManager<IdentityRole> roleManager)
{
    private readonly IUserProfileRepository _userProfileRepository = userProfileRepository;
    private readonly UserManager<ApplicationUserEntity> _userManager = userManager;
    private readonly RoleManager<IdentityRole> _roleManager = roleManager;

    public async Task<UserResult> CreateUserProfileAsync(UserProfileRegistrationForm form, string roleName = "User")
    {
        if (form == null)
            return new UserResult { Succeeded = false, StatusCode = 400, Error = "Form can't be null." };

        var existingUser = await _userManager.FindByEmailAsync(form.Email);
        if (existingUser != null)
            return new UserResult { Succeeded = false, StatusCode = 400, Error = "Email already exists." };

        try
        {
            await _userProfileRepository.BeginTransactionAsync();
            var (appUser, userProfile) = UserProfileFactory.Create(form);
            var result = await _userManager.CreateAsync(appUser, "Password123!");
            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync(roleName))
                {
                    await _roleManager.CreateAsync(new IdentityRole(roleName));
                }

                var addRoleResult = await AddToUserRoleAsync(appUser.Id, roleName);
                if (!addRoleResult.Succeeded)
                {
                    await _userProfileRepository.RollbackTransactionAsync();
                    return new UserResult { Succeeded = false, StatusCode = 500, Error = "Unable to add user to role" };
                }

                await _userProfileRepository.AddAsync(userProfile);
                await _userProfileRepository.CommitTransactionAsync();
                return new UserResult { Succeeded = true, StatusCode = 201 };
            }
            else
            {
                await _userProfileRepository.RollbackTransactionAsync();
                return new UserResult { Succeeded = false, StatusCode = 500, Error = "Unable to create user" };
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return new UserResult { Succeeded = false, StatusCode = 500, Error = "An error occurred while creating the user" };
        }
    }





    //Help Class
    public async Task<UserResult> AddToUserRoleAsync(string userId, string roleName)
    {
        if (!await _roleManager.RoleExistsAsync(roleName))
            return new UserResult { Succeeded = false, StatusCode = 404, Error = "Role does not exist" };

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return new UserResult { Succeeded = false, StatusCode = 404, Error = "User not found" };

        var result = await _userManager.AddToRoleAsync(user, roleName);
            return result.Succeeded
                ? new UserResult { Succeeded = true, StatusCode = 200 }
                : new UserResult { Succeeded = false, StatusCode = 500, Error = "Unable to add user to role" };
    }
}
