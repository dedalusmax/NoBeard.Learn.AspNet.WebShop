using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NoBeard.Learn.AspNet.WebShop.App.Data;
using NoBeard.Learn.AspNet.WebShop.App.Extensions;
using NoBeard.Learn.AspNet.WebShop.App.Models;

namespace NoBeard.Learn.AspNet.WebShop.App.Controllers;

[Authorize]
public class OrderController(ApplicationDbContext context) : BaseController(context)
{
    public IActionResult Index(bool? success)
    {
        ViewData["Success"] = success;

        //var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        //var user = context.Users.Find(userId);

        //var userEmail = HttpContext.User.Identity.Name; // name = email ??
        //var user = context.Users.FirstOrDefault(u => u.Email == userEmail);

        var order = new Order();

        var user = GetUser();
        if (user != null)
        {
            order.CustomerFirstName = string.IsNullOrEmpty(user.FirstName) ? string.Empty : user.FirstName;
            order.CustomerLastName = string.IsNullOrEmpty(user.LastName) ? string.Empty : user.LastName;
            order.CustomerEmailAddress = string.IsNullOrEmpty(user.Email) ? string.Empty : user.Email;
            order.CustomerPhoneNumber = string.IsNullOrEmpty(user.PhoneNumber) ? string.Empty : user.PhoneNumber;
            order.CustomerAddress = string.IsNullOrEmpty(user.Address) ? string.Empty : user.Address;
        }

        var cart = HttpContext.Session.GetCart();

        ViewData["Cart"] = cart;

        return View(order);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("CustomerFirstName,CustomerLastName,CustomerEmailAddress,CustomerPhoneNumber,CustomerAddress")] Order order)
    {
        ModelState.Remove("Items");
        ModelState.Remove("UserId");
        ModelState.Remove("User");

        if (ModelState.IsValid)
        {
            var cart = HttpContext.Session.GetCart();

            // TODO: introduce a transaction here to ensure that the order and its items are saved atomically

            using var transaction = context.Database.BeginTransaction();

            try
            {              
                order.Total = cart.GrandTotal;
                order.DateTimeCreated = DateTime.UtcNow;
                order.UserId = GetUser()!.Id;

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
        else
        {
            var errors = new List<string>();
            foreach (var modelState in ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    errors.Add(error.ErrorMessage);
                }
            }

            //return BadRequest(new { errors });
            return RedirectToAction(nameof(Index), new { success = false, errors });
        }
    }

    [HttpPost]
    public IActionResult RemoveFromCart(int productId)
    {
        var cart = HttpContext.Session.GetCart();

        if (cart.Items.Any(x => x.Product.Id == productId))
        {
            var item = cart.Items.Single(x => x.Product.Id == productId);
            cart.Items.Remove(item);
        }

        HttpContext.Session.SetCart(cart);

        return RedirectToAction(nameof(Index));
    }

}
