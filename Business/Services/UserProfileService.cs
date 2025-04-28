using Business.Factories;
using Business.Model;
using Data.Entities;
using Data.Interfaces;
using Domain.Extensions;
using Domain.Models;
using Domain.Models.UserProfileData;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

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

    public async Task<UserResult> GetAllUsersAsync(bool orderByDescending = false)
    {
        try
        {
            var repositoryResult = await _userProfileRepository.GetAllAsync(orderByDescending: orderByDescending, sortBy: user => user.FirstName!);

            if (!repositoryResult.Succeeded || repositoryResult.Result == null || !repositoryResult.Result.Any())
                return new UserResult { Succeeded = false, StatusCode = 404, Error = "No users found", Users = [] };

            var userProfiles = repositoryResult.Result.ToList();

            var userIds = userProfiles.Select(x => x.Id).ToList();
            var appUsers = await _userManager.Users.Where(u => userIds.Contains(u.Id)).ToListAsync();

            var users = new List<UserProfile>();

            foreach (var profile in userProfiles)
            {
                var appUser = appUsers.FirstOrDefault(u => u.Id == profile.Id);
                if (appUser != null)
                {
                    var enrichedUser = UserProfileFactory.ModelOverload(appUser, profile);
                    users.Add(enrichedUser);
                }
                else
                {
                    return new UserResult { Succeeded = false, StatusCode = 404, Error = $"User not found for profile ID: {profile.Id}" };
                }
            }

            return new UserResult { Succeeded = true, StatusCode = 200, Users = users };
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return new UserResult { Succeeded = false, StatusCode = 500, Error = $"An error occurred while retrieving users: {ex.Message}" };
        }
    }

    public async Task<UserResult> GetUserProfileByIdAsync(string userId)
    {
        try
        {
            var userProfileEntity = await _userProfileRepository.GetEntityAsync(x => x.Id == userId);

            if (userProfileEntity != null && userProfileEntity.ApplicationUser != null)
            {
                var user = UserProfileFactory.ModelOverload(userProfileEntity.ApplicationUser, userProfileEntity.MapTo<UserProfile>());
                return new UserResult { Succeeded = true, StatusCode = 200, Users = [user] };
            }
            else
            {
                return new UserResult { Succeeded = false, StatusCode = 404, Error = "User not found" };
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return new UserResult { Succeeded = false, StatusCode = 500, Error = "An error occurred while retrieving the user" };
        }
    }


    public async Task<UserResult> GetUserProfileByEmailAsync(string email)
    {
        try
        {
            var existingUser = await _userProfileRepository.ExistsAsync(user => user.ApplicationUser.Email == email);
            if (existingUser.Succeeded)
                return new UserResult { Succeeded = true, StatusCode = 200, Error = "User exists with email" };

            return new UserResult { Succeeded = false, StatusCode = 404, Error = "User not found" };
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return new UserResult { Succeeded = false, StatusCode = 500, Error = "An error occurred while checking the user" };
        }
    }

    public async Task<UserResult> UpdateUserProfileAsync(string userId, UserProfileUpdateForm updateFormModel)
    {
        try
        {
            await _userProfileRepository.BeginTransactionAsync();
            var appUser = await _userManager.FindByIdAsync(userId);
            if (appUser == null)
                return new UserResult { Succeeded = false, StatusCode = 404, Error = "User not found" };

            var userProfileEntity = await _userProfileRepository.GetEntityAsync(x => x.Id == userId);
            if (userProfileEntity == null)
                return new UserResult { Succeeded = false, StatusCode = 404, Error = "User profile not found" };


            UserProfileFactory.UpdateEntity(appUser, userProfileEntity, updateFormModel);

            var updateUserProfileResult = await _userProfileRepository.UpdateAsync(userProfileEntity);
            var updateAppUserResult = await _userManager.UpdateAsync(appUser);

            if (!updateUserProfileResult.Succeeded || !updateAppUserResult.Succeeded)
            {
                await _userProfileRepository.RollbackTransactionAsync();
                return new UserResult { Succeeded = false, StatusCode = 500, Error = "Unable to update user" };
            }

            await _userProfileRepository.CommitTransactionAsync();
            return new UserResult { Succeeded = true, StatusCode = 200 };

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            await _userProfileRepository.RollbackTransactionAsync();
            return new UserResult { Succeeded = false, StatusCode = 500, Error = "An error occurred while updating the user" };
        }

    }

    public async Task<UserResult> DeleteUserProfileAsync(string userId)
    {
        try
        {
            await _userProfileRepository.BeginTransactionAsync();

            var userProfileEntity = await _userProfileRepository.GetEntityAsync(x => x.Id == userId);
            if (userProfileEntity == null)
                return new UserResult { Succeeded = false, StatusCode = 404, Error = "User profile not found" };

            var appUser = await _userManager.FindByIdAsync(userId);
            if (appUser == null)
                return new UserResult { Succeeded = false, StatusCode = 404, Error = "User not found" };

            var deleteAppUserResult = await _userManager.DeleteAsync(appUser);
            var deleteUserProfileResult = await _userProfileRepository.DeleteAsync(userProfileEntity);
            

            if(!deleteUserProfileResult.Succeeded || !deleteAppUserResult.Succeeded)
            {
                await _userProfileRepository.RollbackTransactionAsync();
                return new UserResult { Succeeded = false, StatusCode = 500, Error = "Unable to delete user" };
            }
            await _userProfileRepository.CommitTransactionAsync();
            return new UserResult { Succeeded = true, StatusCode = 200 };

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            await _userProfileRepository.RollbackTransactionAsync();
            return new UserResult { Succeeded = false, StatusCode = 500, Error = "An error occurred while deleting the user" };
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
