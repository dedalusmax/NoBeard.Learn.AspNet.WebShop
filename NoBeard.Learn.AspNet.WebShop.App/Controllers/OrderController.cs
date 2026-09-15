using Microsoft.AspNetCore.Mvc;
using NoBeard.Learn.AspNet.WebShop.App.Data;
using NoBeard.Learn.AspNet.WebShop.App.Extensions;
using NoBeard.Learn.AspNet.WebShop.App.Models;

namespace NoBeard.Learn.AspNet.WebShop.App.Controllers;

public class OrderController(ApplicationDbContext context) : Controller
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
    public IActionResult Create([Bind("CustomerFirstName,CustomerLastName,CustomerEmailAddress,CustomerPhoneNumber,CustomerAddress")] Order order)
    {
        ModelState.Remove("Items");

        if (ModelState.IsValid)
        {
            var cart = HttpContext.Session.GetCart();

            // TODO: introduce a transaction here to ensure that the order and its items are saved atomically

            using var transaction = context.Database.BeginTransaction();

            try
            {              
                order.Total = cart.GrandTotal;

                context.Orders.Add(order);
                context.SaveChanges();

                if (cart.Items.Count > 0)
                {
                    foreach (var cartItem in cart.Items)
                    {
                        var item = new OrderItem
                        {
                            OrderId = order.Id,
                            ProductId = cartItem.Product.Id,
                            Quantity = cartItem.Quantity,
                            Price = cartItem.Product.Price,
                            Total = cartItem.Total
                        };

                        context.OrderItems.Add(item);
                    }
                    context.SaveChanges();
                }

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return BadRequest(ex.Message);
            }

            HttpContext.Session.ClearCart();

            return RedirectToAction(nameof(Index), new { success = true });
        }

        return View();
    }
}
