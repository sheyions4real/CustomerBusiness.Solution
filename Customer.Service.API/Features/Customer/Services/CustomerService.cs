using Customer.Service.API.Data.Models;
using Customer.Service.API.Features.Customer.DTO;
using Customer.Service.API.Features.Customer.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using CustomerModel = Customer.Service.API.Data.Models.Customer;

namespace Customer.Service.API.Features.Customer.Services
{
    public class CustomerService(
        AppDbContext dbContext) : ICustomerService
    {
        public readonly AppDbContext _appDbContext = dbContext;

        public async Task<CustomerModel> CreateCustomerAsync(CreateCustomerDto customerDto, CancellationToken cancellationToken)
        {
            var customer = CustomerModel.Create(customerDto);

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
            dbContext.Customers.Add(customer);
            await dbContext.SaveChangesAsync(cancellationToken);


            var createdCustomer = await GetCustomerByIdAsync(customer.CustomerId, cancellationToken);
            if (createdCustomer is null) return null;
            return createdCustomer;

        }

        public async Task<IEnumerable<CustomerModel>> GetAllCustomersAsync(CancellationToken cancellationToken)
        {
            var customers = await _appDbContext.Customers.ToListAsync(cancellationToken);
           return customers;
        }

        public async Task<CustomerModel?> GetCustomerByIdAsync(Guid customerId, CancellationToken cancellationToken)
        {
            var customer = await _appDbContext.Customers
                .Include(c => c.Addresses)
                .Include(c => c.Business)
                .Where(c => c.CustomerId == customerId)
                .FirstOrDefaultAsync(cancellationToken);
            if (customer is null) return null;
            return customer;
        }
    }
}
