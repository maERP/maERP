#nullable disable

using System.ComponentModel.DataAnnotations;

namespace maERP.Server.Models
{
    public class CustomerAddress
    {
        [Key]
        public int Id { get; set; }

        // [Required]
        //public Enum Type { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public String Street { get; set; }

        [Required]
        public String HouseNr { get; set; }

        [Required]
        public String Zip { get; set; }

        [Required]
        public String City { get; set; }

        [Required]
        public int CountryId { get; set; }
    }
}

