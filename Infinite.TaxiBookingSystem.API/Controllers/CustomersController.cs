using Infinite.TaxiBookingSystem.API.Models;
using Infinite.TaxiBookingSystem.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infinite.TaxiBookingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IRepository<Customer> _repository;
        private readonly IGetRepository<Customer> _getRepository;

        public CustomersController(IRepository<Customer> repository,IGetRepository<Customer> getRepository)
        {
            _repository = repository;
            _getRepository = getRepository;
        }

        [HttpGet("GetAllCustomers")]
        public IEnumerable<Customer> GetAllCustomers()
        {
            return _getRepository.GetAll();
        }

        [HttpGet("GetCustomerById/{id}", Name ="GetCustomerById")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var customer = await _getRepository.GetById(id);
            if(customer != null)
            {
                return Ok(customer);
            }
            return NotFound("Customer not found");
        }

        [HttpPost("CreateCustomer")]
        public async Task<IActionResult> CreateCustomer([FromBody]Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _repository.Create(customer);
            return CreatedAtRoute("GetCustomerById", new { id = customer.CustomerId }, customer);
        }

        [HttpPut("UpdateCustomer/{id}")]
        public async Task<IActionResult> UpdateCustomer(int id,Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _repository.Update(id, customer);
            if(result != null)
            {
                return NoContent();
            }
            return NotFound("Customer not found");
        }

        [HttpDelete("DeleteCustomer/{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var result = await _repository.Delete(id);
            if(result != null)
            {
                return Ok();
            }
            return NotFound("Customer not found");
        }
    }
}
