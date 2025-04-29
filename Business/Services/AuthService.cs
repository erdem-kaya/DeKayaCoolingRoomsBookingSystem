using Business.Interfaces;
using Business.Model;
using Data.Entities;
using Data.Interfaces;
using Domain.Models.Auth;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;
using System.Security.Claims;

namespace Business.Services;

public class AuthService(SignInManager<ApplicationUserEntity> signInManager, UserManager<ApplicationUserEntity> userManager, IUserProfileRepository userProfileRepository) : IAuthService
{
    private readonly SignInManager<ApplicationUserEntity> _signInManager = signInManager;
    private readonly UserManager<ApplicationUserEntity> _userManager = userManager;
    private readonly IUserProfileRepository _userProfileRepository = userProfileRepository;

    public async Task<AuthResult> SignInAsync(SignInFormData formData)
    {
        if (formData == null)
            return new AuthResult { Succeeded = false, StatusCode = 400, Error = "Form data is null" };
        try
        {
            var result = await _signInManager.PasswordSignInAsync(formData.Email, formData.Password, formData.IsPersistent, false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(formData.Email);
                if (user != null)
                {
                    var userProfile = await _userProfileRepository.GetAsync(u => u.Id == user.Id);
                    if (userProfile != null && userProfile.Result != null)
                    {
                        var displayName = $"{userProfile.Result.FirstName} {userProfile.Result.LastName}";
                        await AddClaimByEmailAsync(user, "DisplayName", displayName);
                    }
                }
                return new AuthResult { Succeeded = true, StatusCode = 200 };
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return new AuthResult { Succeeded = false, StatusCode = 500, Error = ex.Message };
        }
        return new AuthResult { Succeeded = false, StatusCode = 401, Error = "Invalid email or password" };
    }

    public async Task<AuthResult> SignOutAsync()
    {
        try
        {
            await _signInManager.SignOutAsync();
            return new AuthResult { Succeeded = true, StatusCode = 200 };
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return new AuthResult { Succeeded = false, StatusCode = 500, Error = ex.Message };
        }
    }

    //Helper methods
    public async Task AddClaimByEmailAsync(ApplicationUserEntity user, string typeName, string value)
    {
        // Claim check
        if (user != null)
        {
            var claims = await _userManager.GetClaimsAsync(user);

            if (!claims.Any(x => x.Type == typeName))
            {
                await _userManager.AddClaimAsync(user, new Claim(typeName, value));
            }
        }
    }

}
