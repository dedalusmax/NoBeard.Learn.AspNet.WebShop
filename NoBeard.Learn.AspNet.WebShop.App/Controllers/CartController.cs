using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NoBeard.Learn.AspNet.WebShop.App.Controllers;

[Authorize]
public class CartController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
