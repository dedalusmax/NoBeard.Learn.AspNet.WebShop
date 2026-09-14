using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoBeard.Learn.AspNet.WebShop.App.Models;

namespace NoBeard.Learn.AspNet.WebShop.App.Controllers;

[Authorize]
public class CartController : Controller
{
    public IActionResult Index()
    {
        var cart = new Cart();

        return View(cart);
    }

    [HttpPost]
    public IActionResult AddToCart(int productId)
    {
        return RedirectToAction(nameof(Index));
    }
}
