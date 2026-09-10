using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NoBeard.Learn.AspNet.WebShop.App.Data;

namespace NoBeard.Learn.AspNet.WebShop.App.Controllers;

public class ProductsController(ApplicationDbContext context) : Controller
{
    public IActionResult Index()
    {
        ViewData["Categories"] = new SelectList(context.Categories, "Id", "Name");

        var products = context.Products.ToList();

        return View(products);
    }
}
