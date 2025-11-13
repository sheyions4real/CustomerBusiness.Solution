using System.ComponentModel.DataAnnotations;

namespace CustomerBusiness.Solution.Data.Models
{
    public class Address
    {
        [Key]
        public Guid AddressId { get; set; }

        [MaxLength(255)]
        public string Street { get; set; }

        [MaxLength(100)]
        public string City { get; set; }

        [MaxLength(100)]
        public string Province { get; set; }

        [MaxLength(20)]
        public string PostalCode { get; set; }

        [MaxLength(100)]
        public string Country { get; set; }
    }
}
