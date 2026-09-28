using NoBeard.Learn.AspNet.WebShop.App.Models;
using System.Text.Json;

namespace NoBeard.Learn.AspNet.WebShop.App.Extensions;

public static class ISessionExtensions
{
    private const string CART_SESSION_KEY = "_cart";

    public static void SetCart(this ISession session, Cart cart)
    {
        var sessionData = JsonSerializer.Serialize(cart);
        session.SetString(CART_SESSION_KEY, sessionData);
    }

    public static Cart GetCart(this ISession session)
    {
        var sessionData = session.GetString(CART_SESSION_KEY);
        if (string.IsNullOrEmpty(sessionData))
        {
            return new Cart();
        }
        else
        {
            return JsonSerializer.Deserialize<Cart>(sessionData)!;
        }
    }

    public static void ClearCart(this ISession session)
    {
        session.Remove(CART_SESSION_KEY);
    }
}
