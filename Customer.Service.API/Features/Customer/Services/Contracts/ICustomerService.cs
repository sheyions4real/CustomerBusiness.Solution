using Customer.Service.API.Features.Customer.DTO;
using CustomerModel = Customer.Service.API.Data.Models.Customer;

namespace Customer.Service.API.Features.Customer.Services.Contracts
{
    public interface ICustomerService
    {
        Task<CustomerModel> GetCustomerByIdAsync(Guid customerId, CancellationToken cancellationToken);
        Task<IEnumerable<CustomerModel>> GetAllCustomersAsync(CancellationToken cancellationToken);
        Task<CustomerModel> CreateCustomerAsync(CreateCustomerDto customerDto, CancellationToken cancellationToken);
    }
}
