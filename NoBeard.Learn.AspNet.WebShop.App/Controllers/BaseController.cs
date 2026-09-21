using Microsoft.AspNetCore.Mvc;
using NoBeard.Learn.AspNet.WebShop.App.Data;
using NoBeard.Learn.AspNet.WebShop.App.Models;
using System.Security.Claims;

namespace NoBeard.Learn.AspNet.WebShop.App.Controllers;

public abstract class BaseController(ApplicationDbContext context) : Controller
{
    protected ApplicationUser? GetUser()
    {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return context.Users.Find(userId);
    }
}
