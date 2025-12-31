using System.ComponentModel.DataAnnotations;

namespace Customer.Service.API.Features.Customer.DTO
{
    public class CreateAddressDto
    {
        [MaxLength(255)]
        public string? Street { get; set; }

        [MaxLength(100)]
        public string? City { get; set; }

        [MaxLength(100)]
        public string? Province { get; set; }

        [MaxLength(20)]
        public string? PostalCode { get; set; }

        [MaxLength(100)]
        public string? Country { get; set; }
        public bool IsPrimary { get; set; } = false;
    }
}
