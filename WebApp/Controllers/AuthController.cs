using Business.Interfaces;
using Domain.Extensions;
using Domain.Models.Auth;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;

public class AuthController(IAuthService authService) : Controller
{
    private readonly IAuthService _authService = authService;

    public IActionResult SignInPage(string returnUrl = "~/")
    {
        ViewBag.ReturnUrl = returnUrl;

        var model = new SignInViewModel
        {
            Title = "Giriş Yap",
            ErrorMessages = "",
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> SignInPage(SignInViewModel model, string returnUrl = "~/")
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState
                .Where(x => x.Value?.Errors.Count > 0)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage).ToArray());

            return BadRequest(new { success = false, errors });
        }

        var signInFormData = model.MapTo<SignInFormData>();
        var result = await _authService.SignInAsync(signInFormData);

        if (result.Succeeded)
        {
            return LocalRedirect(returnUrl);
        }
        if (result.StatusCode == 401)
        {
            ModelState.AddModelError(string.Empty, "Email or password is incorrect.");
            model.ErrorMessages = "Email or password is incorrect.";
            return View(model);
        }
       
        ModelState.AddModelError(string.Empty, "An unknown error occurred.");
        model.ErrorMessages = "An unknown error occurred.";
        return View(model);

    }
}
