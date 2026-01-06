using Customer.Service.API.Data.Repository.BaseRepository.Interface;
using Customer.Service.API.Features.Customer.DTO;
using CustomerModel = Customer.Service.API.Data.Models.Customer;

namespace Customer.Service.API.Data.Repository.CustomerRepository
{
    public interface ICustomerRepository : IRepository<CustomerModel>
    {
        Task<CustomerModel> GetCustomerIncludeAddressAsync(Guid customerId, CancellationToken cancellationToken);
        Task<CustomerModel> CreateCustomerAsync(CreateCustomerDto customerDto, CancellationToken cancellationToken);
        Task<CustomerModel?> UpdateCustomerAsync(Guid customerId, UpdateCustomerDto customerDto, CancellationToken cancellationToken);
        Task<bool?> DeleteCustomerAsync(Guid customerId, CancellationToken cancellationToken);

    }
}
