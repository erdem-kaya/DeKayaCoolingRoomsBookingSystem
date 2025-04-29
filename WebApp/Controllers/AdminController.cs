using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

public class AdminController : Controller
{
    [Authorize]
    public IActionResult Index()
    {
        return View();
    }
}
