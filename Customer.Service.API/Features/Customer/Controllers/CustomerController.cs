using Customer.Service.API.Data.Models;
using Customer.Service.API.Features.Customer.DTO;
using Customer.Service.API.Features.Customer.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CustomerModel = Customer.Service.API.Data.Models.Customer;

namespace Customer.Service.API.Features.Customer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
        {
            var customers = await _customerService.GetAllCustomersAsync(cancellationToken);
            return Ok(customers);
        }

        [HttpGet("{id:guid}", Name = "GetCustomerById")]
        public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id, cancellationToken);
            if (customer is null) return NotFound($"Customer with id: {id} not found");

            // to be replace with automapper later
            var response = new CustomerDto
            {
                CustomerId = customer.CustomerId,
                BusinessId = customer.BusinessId,
                FullName = customer.FullName,
                Email = customer.Email,
                Phone = customer.Phone,
                Status = customer.Status,
                Notes = customer.Notes,
                CreatedAt = customer.CreatedAt,
                CreatedBy = customer.CreatedBy,
                ModifiedAt = customer.ModifiedAt,
                ModifiedBy = customer.ModifiedBy,
                PrimaryAddress = customer.Addresses
             .FirstOrDefault(a => a.IsPrimary) is Address primary
             ? new AddressDto
             {
                 AddressId = primary.AddressId,
                 Street = primary.Street,
                 City = primary.City,
                 Province = primary.Province,
                 PostalCode = primary.PostalCode,
                 Country = primary.Country,
                 IsPrimary = primary.IsPrimary
             }
             : null,
                OtherAddresses = customer.Addresses
             .Where(a => !a.IsPrimary)
             .Select(a => new AddressDto
             {
                 AddressId = a.AddressId,
                 Street = a.Street,
                 City = a.City,
                 Province = a.Province,
                 PostalCode = a.PostalCode,
                 Country = a.Country,
                 IsPrimary = a.IsPrimary
             })
             .ToList()
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomerAsync([FromBody] CreateCustomerDto customerDto, CancellationToken cancellationToken)
        {
            var createdCustomer = await _customerService.CreateCustomerAsync(customerDto, cancellationToken);
            if (createdCustomer is null)
                return Problem("Failed to create customer.");

            // to be replace with automapper later
            var response = new CustomerDto
            {
                CustomerId = createdCustomer.CustomerId,
                BusinessId = createdCustomer.BusinessId,
                FullName = createdCustomer.FullName,
                Email = createdCustomer.Email,
                Phone = createdCustomer.Phone,
                Status = createdCustomer.Status,
                Notes = createdCustomer.Notes,
                CreatedAt = createdCustomer.CreatedAt,
                CreatedBy = createdCustomer.CreatedBy,
                ModifiedAt = createdCustomer.ModifiedAt,
                ModifiedBy = createdCustomer.ModifiedBy,
                PrimaryAddress = createdCustomer.Addresses
             .FirstOrDefault(a => a.IsPrimary) is Address primary
             ? new AddressDto
             {
                 AddressId = primary.AddressId,
                 Street = primary.Street,
                 City = primary.City,
                 Province = primary.Province,
                 PostalCode = primary.PostalCode,
                 Country = primary.Country,
                 IsPrimary = primary.IsPrimary
             }
             : null,
                OtherAddresses = createdCustomer.Addresses
             .Where(a => !a.IsPrimary)
             .Select(a => new AddressDto
             {
                 AddressId = a.AddressId,
                 Street = a.Street,
                 City = a.City,
                 Province = a.Province,
                 PostalCode = a.PostalCode,
                 Country = a.Country,
                 IsPrimary = a.IsPrimary
             })
             .ToList()
            };


            return CreatedAtRoute(
                "GetCustomerById",
                new { id = createdCustomer.CustomerId },
                response
            );
        }


        [HttpPut("{customerId:guid}")]
        public async Task<IActionResult> UpdateCustomerAsync([FromRoute] Guid customerId, [FromBody] UpdateCustomerDto customerDto, CancellationToken cancellationToken)
        {
            // Implementation for updating a customer would go here
            if (customerDto == null)
            {
                return BadRequest("Customer data is required.");
            }

            // Save changes
            var updatedCustomer = await _customerService.UpdateCustomerAsync(customerId, customerDto, cancellationToken);
            if (updatedCustomer == null)
            {
                return NotFound($"Customer with id: {customerId} not found.");
            }

            // to be replace with automapper later
            var response = new CustomerDto
            {
                CustomerId = updatedCustomer.CustomerId,
                BusinessId = updatedCustomer.BusinessId,
                FullName = updatedCustomer.FullName,
                Email = updatedCustomer.Email,
                Phone = updatedCustomer.Phone,
                Status = updatedCustomer.Status,
                Notes = updatedCustomer.Notes,
                CreatedAt = updatedCustomer.CreatedAt,
                CreatedBy = updatedCustomer.CreatedBy,
                ModifiedAt = updatedCustomer.ModifiedAt,
                ModifiedBy = updatedCustomer.ModifiedBy,
                PrimaryAddress = updatedCustomer.Addresses
             .FirstOrDefault(a => a.IsPrimary) is Address primary
             ? new AddressDto
             {
                 AddressId = primary.AddressId,
                 Street = primary.Street,
                 City = primary.City,
                 Province = primary.Province,
                 PostalCode = primary.PostalCode,
                 Country = primary.Country,
                 IsPrimary = primary.IsPrimary
             }
             : null,
                OtherAddresses = updatedCustomer.Addresses
             .Where(a => !a.IsPrimary)
             .Select(a => new AddressDto
             {
                 AddressId = a.AddressId,
                 Street = a.Street,
                 City = a.City,
                 Province = a.Province,
                 PostalCode = a.PostalCode,
                 Country = a.Country,
                 IsPrimary = a.IsPrimary
             })
             .ToList()
            };


            return Ok(response);
        }



        [HttpDelete("{customerId:guid}")]
        public async Task<IActionResult> DeleteCustomerAsync(Guid customerId, CancellationToken cancellationToken)
        {
            // Implementation for deleting a customer would go here
            var deleted = await _customerService.DeleteCustomerAsync(customerId, cancellationToken);
            if (deleted is null)
            {
                return NotFound($"Customer with id: {customerId} not found.");
            }

            if (deleted == false)
            {
                return Problem("Failed to delete customer.");
            }

            return Ok(deleted);
        }
    }
}
