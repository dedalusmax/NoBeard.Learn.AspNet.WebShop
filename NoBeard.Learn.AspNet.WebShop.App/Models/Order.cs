using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NoBeard.Learn.AspNet.WebShop.App.Models;

public class Order
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "decimal(9,2)")]
    public decimal Total { get; set; }

    [Required]
    public DateTime DateTimeCreated { get; set; }

    #region Personal Information

    [Required(ErrorMessage = "First name is required.")]
    [DisplayName("Customer's First Name")]
    [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
    public string CustomerFirstName { get; set; }
    
    [Required(ErrorMessage = "Last name is required.")]
    [DisplayName("Customer's Last Name")]
    [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")]
    public string CustomerLastName { get; set; }

    [EmailAddress]
    [Required(ErrorMessage = "Email address is required.")]
    [DisplayName("Customer's Email Address")]
    [StringLength(100, ErrorMessage = "Email address cannot exceed 100 characters.")]
    public string CustomerEmailAddress { get; set; }

    [Phone]
    [Required(ErrorMessage = "Phone number is required.")]
    [DisplayName("Customer's Phone Number")]
    [StringLength(20, ErrorMessage = "Phone number cannot exceed 20 characters.")]
    public string CustomerPhoneNumber { get; set; }

    [Required(ErrorMessage = "Address is required.")]
    [DisplayName("Customer's Address")]
    [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters.")]
    public string CustomerAddress { get; set; }

    #endregion

    [ForeignKey("OrderId")]
    public virtual ICollection<OrderItem> Items { get; set; } 
}
