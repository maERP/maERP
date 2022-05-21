#nullable disable

using System.ComponentModel.DataAnnotations;

namespace maERP.Server.Data
{
	public class TaxClass
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public Double TaxRate { get; set; }
 
		// public virtual IList<Product> Products { get; set; }
	}
}