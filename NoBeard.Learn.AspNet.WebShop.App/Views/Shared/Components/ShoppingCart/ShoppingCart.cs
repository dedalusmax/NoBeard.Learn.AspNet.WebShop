using Microsoft.AspNetCore.Mvc;
using NoBeard.Learn.AspNet.WebShop.App.Extensions;

namespace NoBeard.Learn.AspNet.WebShop.App.Views.Shared.Components.ShoppingCart;

//[ViewComponent(Name = "Cart")]
public class ShoppingCart : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        var cart = HttpContext.Session.GetCart();

        return View(cart);
    }
}
