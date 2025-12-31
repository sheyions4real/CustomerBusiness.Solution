using Customer.Service.API.Features.Customer.DTO;
using System.Net;

namespace Customer.Service.API.Data.Models
{
    public partial class Customer
    {
        // Custom methods or properties can be added here
        public static Customer Create(CreateCustomerDto customerDto)
        {
            if (customerDto == null)
                throw new ArgumentNullException(nameof(customerDto));

            return new Customer
            {
                CustomerId = Guid.NewGuid(),
                BusinessId = customerDto.BusinessId,
                FullName = customerDto.FullName,
                Email = customerDto.Email,
                Phone = customerDto.Phone,
                Notes = customerDto.Notes,
                Status = customerDto.Status ?? "Active",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = customerDto.CreatedBy,
                Addresses = new List<Address>()
            };
        }

        public void AddCustomerAddresses(CreateAddressDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            var addressEntity = Address.Create(dto, CustomerId);

            if (dto.IsPrimary || !Addresses.Any())
            {
                foreach (var address in Addresses)
                    address.IsPrimary = false;

                addressEntity.IsPrimary = true;
            }

            Addresses.Add(addressEntity);
        }
    }
}
