#nullable disable

using System.ComponentModel.DataAnnotations;

namespace maERP.Shared.Models
{
    public class Product
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string SKU { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(32)]
        public string EAN { get; set; }

        [StringLength(32)]
        public string ASIN { get; set; }

        [StringLength(64000)]
        public string Description { get; set; }

        // [Required]
        // [Column(TypeName = "money")]
        public decimal Price { get; set; }

        // [Required]
        // [ForeignKey("TaxClass")]
        public int TaxClassId { get; set; }
        public TaxClass TaxClass { get; set; }

        public int ProductSalesChannelId { get; set; }
        public IList<ProductSalesChannel> ProductSalesChannel { get; set; }

        public int ProductStockId { get; set; }
        public IList<ProductStock> ProductStock { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}