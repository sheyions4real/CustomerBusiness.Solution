using Customer.Service.API.Data.Models;
using Customer.Service.API.Data.Repository.CustomerRepository;
using Customer.Service.API.Features.Customer.DTO;
using Customer.Service.API.Features.Customer.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using CustomerModel = Customer.Service.API.Data.Models.Customer;

namespace Customer.Service.API.Features.Customer.Services
{
    public class CustomerService(ICustomerRepository customerRepository) : ICustomerService
    {
        public readonly ICustomerRepository _customerRepository = customerRepository;

        public async Task<CustomerModel> CreateCustomerAsync(CreateCustomerDto customerDto, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.CreateCustomerAsync(customerDto, cancellationToken);
            return customer;
        }

        public async Task<bool?> DeleteCustomerAsync(Guid customerId, CancellationToken cancellationToken)
        {
           return await _customerRepository.DeleteCustomerAsync(customerId, cancellationToken);
        }

        public async Task<IEnumerable<CustomerModel>> GetAllCustomersAsync(CancellationToken cancellationToken)
        {
            var customers = await _customerRepository.GetAllAsync(cancellationToken);
           return customers;
        }

        public async Task<CustomerModel?> GetCustomerByIdAsync(Guid customerId, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetCustomerIncludeAddressAsync(customerId, cancellationToken);
            return customer;
        }

        public async Task<CustomerModel?> UpdateCustomerAsync(Guid customerId,UpdateCustomerDto customerDto, CancellationToken cancellationToken)
        {
            var existingCustomer = await _customerRepository.UpdateCustomerAsync(customerId, customerDto, cancellationToken);

            return existingCustomer;
        }
             
    }
}
