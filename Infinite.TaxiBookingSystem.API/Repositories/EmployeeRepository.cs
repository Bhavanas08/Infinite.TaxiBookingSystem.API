using Infinite.TaxiBookingSystem.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infinite.TaxiBookingSystem.API.Repositories
{
    public class EmployeeRepository : IRepository<Employee>,IGetRepository<Employee>
    {
        private readonly ApplicationDbContext _Context;


        public EmployeeRepository(ApplicationDbContext context)
        {
            _Context = context;
        }
        public async Task Create(Employee obj)
        {
            if (obj != null)
            {
                _Context.Employees.Add(obj);
                await _Context.SaveChangesAsync();
            }
        }

        public async Task<Employee> Delete(int id)
        {
            var employeeDb = await _Context.Employees.FindAsync(id);
            if (employeeDb != null)
            {
                _Context.Employees.Remove(employeeDb);
                await _Context.SaveChangesAsync();
                return employeeDb;
            }
            return null;
        }

        public IEnumerable<Employee> GetAll()
        {
            return _Context.Employees.ToList();
        }

        public async Task<Employee> GetById(int id)
        {
            var employee = await _Context.Employees.FindAsync(id);
            if(employee != null)
            {
                return employee;
            }
            return null;
        }

        

        public async Task<Employee> Update(int id, Employee obj)
        {
            var employeeDb = await _Context.Employees.FindAsync(id);
            if (employeeDb != null)
            {
                employeeDb.EmployeeName = obj.EmployeeName;
                employeeDb.Designation = obj.Designation;
                employeeDb.PhoneNo = obj.PhoneNo;
                employeeDb.EmailId = obj.EmailId;
                employeeDb.Address = obj.Address;
                //employeeDb.DrivingLicenseNo = obj.DrivingLicenseNo;
                _Context.Employees.Update(employeeDb);
                await _Context.SaveChangesAsync();
                return employeeDb;
            }
            return null;
        }
        
    }
}
