using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Balofinos_Midterm_Store.Models
{
    public class CartItem
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        [Required]
        public string ProductName { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Range(1, 99, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }
    }
}