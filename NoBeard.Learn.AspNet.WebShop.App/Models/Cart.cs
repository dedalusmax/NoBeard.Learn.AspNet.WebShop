namespace NoBeard.Learn.AspNet.WebShop.App.Models;

public record Cart
{
    public List<CartItem> Items { get; set; } = [];

    public decimal GrandTotal => Items.Count == 0 ? 0 : Items.Sum(x => x.Total);
}

public record CartItem
{
    public Product Product { get; set; }

    public decimal Quantity { get; set; }

    public decimal Total => Product.Price * Quantity;
}