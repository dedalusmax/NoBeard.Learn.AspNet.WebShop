using Microsoft.AspNetCore.Mvc;
using NoBeard.Learn.AspNet.WebShop.App.Extensions;
using NoBeard.Learn.AspNet.WebShop.App.Models;

namespace NoBeard.Learn.AspNet.WebShop.App.Controllers;

public class OrderController : Controller
{
    public IActionResult Index(bool? success)
    {
        ViewData["Success"] = success;

        var cart = HttpContext.Session.GetCart();

        ViewData["Cart"] = cart;

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("CustomerFirstName,CustomerLastName,CustomerEmailAddress,CustomerPhoneNumber,CustomerAddress")] Order order)
    {
        ModelState.Remove("Items");

        if (ModelState.IsValid)
        {




            return RedirectToAction(nameof(Index), new { success = true });
        }

        return View();
    }
}
