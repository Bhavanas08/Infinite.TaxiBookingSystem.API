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
    public class EmployeesController : ControllerBase
    {
        private readonly IRepository<Employee> _repository;
        private readonly IGetRepository<Employee> _getRepository;

        public EmployeesController(IRepository<Employee> repository,IGetRepository<Employee> getRepository)
        {
            _repository = repository;
            _getRepository = getRepository;
        }

        [HttpGet("GetAllEmployees")]
        public IEnumerable<Employee> GetEmployees()
        {
            return _getRepository.GetAll();
        }

        [HttpGet("GetEmployeeById/{id}",Name ="GetEmployeeById")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var employee =await _getRepository.GetById(id);
            if (employee!= null)
            {
                return Ok(employee);
            }
            return NotFound("Employee not found");
        }

        [HttpPost("CreateEmployee")]
        public async Task<IActionResult> CreateEmployee([FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _repository.Create(employee);
            return CreatedAtRoute("GetEmployeeById", new { id = employee.EmployeeId }, employee);
        }

        [HttpPut("UpdateEmployee/{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _repository.Update(id, employee);
            if(result != null)
            {
                return NoContent();
            }
            return NotFound("Employee Not Found");
        }
        
        [HttpDelete("DeleteEmployee/{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var result = await _repository.Delete(id);
            if(result!= null)
            {
                return Ok();
            }
            return NotFound("Movie Not Found");
        }
    }
}
