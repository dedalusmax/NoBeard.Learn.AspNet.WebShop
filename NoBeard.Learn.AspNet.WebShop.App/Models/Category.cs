using System.ComponentModel.DataAnnotations;

namespace NoBeard.Learn.AspNet.WebShop.App.Models;

public class Category
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters.")]
    public required string Name { get; set; }

    public required virtual ICollection<ProductCategory> Products { get; set; }
}
