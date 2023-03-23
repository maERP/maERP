#nullable disable

using System.ComponentModel.DataAnnotations;

namespace maERP.Shared.Models;

public class TaxClass
{
	[Key]
	public int Id { get; set; }

	[Required]
	public Double TaxRate { get; set; }

	public DateTime CreatedAt { get; set; }
	public DateTime UpdatedAt { get; set; }

	// public virtual IList<Product> Products { get; set; }
}