#nullable disable

namespace maERP.Server.Models
{
	public class BaseProductDto
	{
        public int Id { get; set; }
        public string SKU { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int TaxClassId { get; set; }
    }
}