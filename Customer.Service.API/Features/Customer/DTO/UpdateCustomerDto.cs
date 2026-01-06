namespace Customer.Service.API.Features.Customer.DTO
{
    public class UpdateCustomerDto
    {
        public Guid BusinessId { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string? Notes { get; set; }
        public string? Status { get; set; }
        public Guid? ModifiedBy { get; set; }

        public List<AddressDto>? Addresses { get; set; } = new();
    }
}
