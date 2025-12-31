using Customer.Service.API.Features.Customer.DTO;

namespace Customer.Service.API.Data.Models
{
    public partial class Address
    {
        public static Address Create(CreateAddressDto dto, Guid customerId)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            return new Address
            {
                CustomerId = customerId,
                Street = dto.Street,
                City = dto.City,
                Province = dto.Province,
                PostalCode = dto.PostalCode,
                Country = dto.Country,
                IsPrimary = dto.IsPrimary
            };
        }
    }

}
