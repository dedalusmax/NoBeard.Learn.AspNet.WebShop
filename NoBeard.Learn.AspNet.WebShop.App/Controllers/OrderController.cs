using Microsoft.AspNetCore.Mvc;
using NoBeard.Learn.AspNet.WebShop.App.Extensions;

namespace NoBeard.Learn.AspNet.WebShop.App.Controllers;

public class OrderController : Controller
{
    public IActionResult Index()
    {
        var cart = HttpContext.Session.GetCart();

        ViewData["Cart"] = cart;

        return View();
    }
}
