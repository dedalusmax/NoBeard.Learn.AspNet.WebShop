using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NoBeard.Learn.AspNet.WebShop.App.Models;

public class ProductCategory
{
    [Key]
    public int Id { get; set; }

    [Required, DisplayName("Product")]
    public int ProductId { get; set; }

    public virtual Product Product { get; set; } = null!;

    [Required, DisplayName("Category")]
    public int CategoryId { get; set; }

    public virtual Category Category { get; set; } = null!;
}
