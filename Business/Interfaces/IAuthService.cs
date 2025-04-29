using Business.Model;
using Data.Entities;
using Domain.Models.Auth;

namespace Business.Interfaces;

public interface IAuthService
{
    Task AddClaimByEmailAsync(ApplicationUserEntity user, string typeName, string value);
    Task<AuthResult> SignInAsync(SignInFormData formData);
    Task<AuthResult> SignOutAsync();
}
