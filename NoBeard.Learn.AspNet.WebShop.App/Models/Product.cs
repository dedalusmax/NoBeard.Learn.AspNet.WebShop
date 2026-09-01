using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NoBeard.Learn.AspNet.WebShop.App.Models;

public class Product
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters.")]
    public required string Name { get; set; }

    [Required]
    [Column(TypeName = "decimal(9,2)")]
    public decimal Price { get; set; }

    public string? Description { get; set; }
    
    public required virtual ICollection<ProductCategory> Categories { get; set; }

    [ForeignKey("ProductId")]
    public required virtual ICollection<OrderItem> OrderItems { get; set; }
}
