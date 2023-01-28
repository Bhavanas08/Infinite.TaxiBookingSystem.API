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
    public class EmployeeRostersController : ControllerBase
    {
        private readonly IRepository<EmployeeRoster> _repository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IGetRepository<EmployeeRosterDto> _employeeRosterDtoRepository;

        public EmployeeRostersController(IRepository<EmployeeRoster> repository,IEmployeeRepository employeeRepository,IGetRepository<EmployeeRosterDto> employeeRosterDtoRepository)
        {
            _repository = repository;
            _employeeRepository = employeeRepository;
            _employeeRosterDtoRepository = employeeRosterDtoRepository;
        }

        [HttpGet("GetAllRosters")]
        public IEnumerable<EmployeeRosterDto> GetRosters()
        {
            return _employeeRosterDtoRepository.GetAll();
        }

        [HttpGet("GetRosterById/{id}" ,Name ="GetRosterById")]
        public async Task<IActionResult> GetRosterById(int id)
        {
            var roster = await _employeeRosterDtoRepository.GetById(id);
            if(roster != null)
            {
                return Ok(roster);
            }
            return NotFound("Roster details not found");
        }

        [HttpPost("CreateRoster")]
        public async Task<IActionResult> CreateRoster([FromBody] EmployeeRoster employeeRoster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _repository.Create(employeeRoster);
            return CreatedAtRoute("GetRosterById", new { id = employeeRoster.Id }, employeeRoster);
        }

        [HttpPut("UpdateRoster/{id}")]
        public async Task<IActionResult> UpdateRoster(int id,[FromBody]EmployeeRoster employeeRoster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _repository.Update(id, employeeRoster);
            if(result != null)
            {
                return NoContent();
            }
            return NotFound("Roster details not found");
        }

        [HttpDelete("DeleteRoster/{id}")]
        public async Task<IActionResult> DeleteRoster(int id)
        {
            var result = await _repository.Delete(id);
            if(result != null)
            {
                return Ok();
            }
            return NotFound("Roster Not found");
        }

        [HttpGet("SearchRoster/{employeeName}")]
        public async Task<IActionResult> SearchRosterByEmployee(string employeeName)
        {
            var result = await _employeeRepository.SearchByEmp(employeeName);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound("Please Provide valid Employee");
        }
    }
}
