namespace Customer.Service.API.Features.Customer.DTO
{
    public class CreateCustomerDto
    {
        public Guid BusinessId { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public CreateAddressDto? PrimaryAddress { get; set; }
        public string? Notes { get; set; }
        public string? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public Guid? ModifiedBy { get; set; }
        public ICollection<CreateAddressDto>? OtherAddresses { get; set; }
    }
}
