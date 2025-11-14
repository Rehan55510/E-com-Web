using System.ComponentModel.DataAnnotations;

namespace E_com_Web.Areas.Admin.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required, DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public int StockQuantity { get; set; }

        public string? ImageUrl { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
    }
}
