using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NoBeard.Learn.AspNet.WebShop.App.Areas.Admin.Models;
using NoBeard.Learn.AspNet.WebShop.App.Data;

namespace NoBeard.Learn.AspNet.WebShop.App.Controllers;

public class ProductsController(ApplicationDbContext context) : BaseController(context)
{
    public IActionResult Index(int? categoryId)
    {
        ViewData["Categories"] = new SelectList(context.Categories, "Id", "Name");

        if (categoryId != null)
        {
            var products = (
                from p in context.Products
                join pc in context.ProductCategories on p.Id equals pc.ProductId
                where pc.CategoryId == categoryId
                select p
                )
                .Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Description = p.Description,
                    FileName = p.FileName,
                    FileContentBase64 = p.FileContent != null ? Convert.ToBase64String(p.FileContent) : null
                })
                .ToList();

            return View(products);
        }
        else
        {
            var products = context.Products
                .Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Description = p.Description,
                    FileName = p.FileName,
                    FileContentBase64 = p.FileContent != null ? Convert.ToBase64String(p.FileContent) : null
                })
                .ToList();

            return View(products);
        }
    }
}
