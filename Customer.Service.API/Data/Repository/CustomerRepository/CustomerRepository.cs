using Customer.Service.API.Data.Models;
using Customer.Service.API.Data.Repository.BaseRepository;
using Customer.Service.API.Data.Repository.BaseRepository.Interface;
using Customer.Service.API.Features.Customer.DTO;
using Microsoft.EntityFrameworkCore;


namespace Customer.Service.API.Data.Repository.CustomerRepository
{
    public class CustomerRepository : Repository<Models.Customer>, ICustomerRepository
    {
        private readonly AppDbContext _appDbContext;

        public CustomerRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<Models.Customer> CreateCustomerAsync(CreateCustomerDto customerDto, CancellationToken cancellationToken)
        {
            var customer = Models.Customer.Create(customerDto);

            // Add primary address
            if (customerDto.PrimaryAddress != null)
                customer.AddCustomerAddresses(customerDto.PrimaryAddress);

            // Add secondary addresses
            if (customerDto.OtherAddresses != null && customerDto.OtherAddresses.Any())
            {
                foreach (var dto in customerDto.OtherAddresses)
                    customer.AddCustomerAddresses(dto);
            }

            // 1️⃣ Save customer + addresses (no PrimaryAddressId yet)
            await _appDbContext.Customers.AddAsync(customer, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);


            var createdCustomer = await GetCustomerIncludeAddressAsync(customer.CustomerId, cancellationToken);
            if (createdCustomer is null) return null;
            return createdCustomer;
        }

        public async Task<bool?> DeleteCustomerAsync(Guid customerId, CancellationToken cancellationToken)
        {
            var customer = await _appDbContext.Customers
               .Include(c => c.Addresses)
               .Where(c => c.CustomerId == customerId)
                .FirstOrDefaultAsync(cancellationToken);

            if (customer is null) return null;
            _appDbContext.Customers.Remove(customer);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<Models.Customer> GetCustomerIncludeAddressAsync(Guid customerId, CancellationToken cancellationToken)
        {
            var customer = await _appDbContext.Customers
                .Include(c => c.Addresses)
                .Include(c => c.Business)
                .Where(c => c.CustomerId == customerId)
                .FirstOrDefaultAsync(cancellationToken);
            if (customer is null) return null;
            return customer;
        }

        public async Task<Models.Customer?> UpdateCustomerAsync(Guid customerId, UpdateCustomerDto customerDto, CancellationToken cancellationToken)
        {
            var existingCustomer = await _appDbContext.Customers
                .Include(c => c.Addresses)
                .FirstOrDefaultAsync(c => c.CustomerId == customerId, cancellationToken);

            if (existingCustomer is null) return null;
            // Update fields
            existingCustomer.FullName = customerDto.FullName;
            existingCustomer.BusinessId = customerDto.BusinessId;
            existingCustomer.Email = customerDto.Email;
            existingCustomer.Phone = customerDto.Phone;
            existingCustomer.Notes = customerDto.Notes;
            existingCustomer.Status = customerDto.Status;
            existingCustomer.ModifiedAt = DateTime.UtcNow;
            existingCustomer.ModifiedBy = customerDto.ModifiedBy;

            // check if addresses need to be updated/added
            if (customerDto.Addresses != null)
            {
                foreach (var addressDto in customerDto.Addresses)
                {
                    // Check if address already exists
                    var existingAddress = existingCustomer.Addresses
                        .FirstOrDefault(a => a.AddressId == addressDto.AddressId);

                    if (existingAddress != null)
                    {
                        // Update existing address
                        existingAddress.Street = addressDto.Street;
                        existingAddress.City = addressDto.City;
                        existingAddress.Province = addressDto.Province;
                        existingAddress.PostalCode = addressDto.PostalCode;
                        existingAddress.Country = addressDto.Country;

                        // Handle primary address logic
                        if (addressDto.IsPrimary)
                        {
                            foreach (var a in existingCustomer.Addresses)
                                a.IsPrimary = false;

                            existingAddress.IsPrimary = true;
                        }
                        else
                        {
                            existingAddress.IsPrimary = existingAddress.IsPrimary; // keep as is
                        }
                    }
                    else
                    {
                        // Add new address
                        if (addressDto.IsPrimary)
                        {
                            // If new primary, reset existing ones
                            foreach (var a in existingCustomer.Addresses)
                                a.IsPrimary = false;
                        }

                        //convert addressDto to createAddressDto
                        var createAddressDto = new CreateAddressDto
                        {
                            Street = addressDto.Street,
                            City = addressDto.City,
                            Province = addressDto.Province,
                            PostalCode = addressDto.PostalCode,
                            Country = addressDto.Country,
                            IsPrimary = addressDto.IsPrimary,
                        };

                        var newAddress = Address.Create(createAddressDto, existingCustomer.CustomerId);
                        existingCustomer.Addresses.Add(newAddress);
                    }
                }
            }

            await _appDbContext.SaveChangesAsync(cancellationToken);

            return existingCustomer;
        }
    }
}
