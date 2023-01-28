using Infinite.TaxiBookingSystem.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infinite.TaxiBookingSystem.API.Repositories
{
    public class EmployeeRosterRepository : IRepository<EmployeeRoster>,IEmployeeRepository, IGetRepository<EmployeeRosterDto>
    {
        public readonly ApplicationDbContext _Context;

        public EmployeeRosterRepository(ApplicationDbContext context)
        {
            _Context = context;
        }

        public async Task Create(EmployeeRoster obj)
        {
            if(obj != null)
            {
                _Context.EmployeeRosters.Add(obj);
                await _Context.SaveChangesAsync();
            }
        }

        public async Task<EmployeeRoster> Delete(int id)
        {
            var rosterDb = await _Context.EmployeeRosters.FindAsync(id);
            if(rosterDb != null)
            {
                _Context.EmployeeRosters.Remove(rosterDb);
                await _Context.SaveChangesAsync();
                return rosterDb;
            }
            return null;
        }

        public IEnumerable<EmployeeRosterDto> GetAll()
        {
            //return _Context.EmployeeRosters.ToList();
            var employeerosters = _Context.EmployeeRosters.Include(x => x.Employee).Select(x => new EmployeeRosterDto
            {
                Id = x.Id,
                EmployeeId = x.EmployeeId,
                FromDate = x.FromDate,
                ToDate = x.ToDate,
                InTime = x.InTime,
                OutTime = x.OutTime,
                EmployeeName = x.Employee.EmployeeName
            }).ToList();
            return employeerosters;
        }

        public async Task<EmployeeRosterDto> GetById(int id)
        {
            var rosters = await _Context.EmployeeRosters.Include(x => x.Employee).Select(x => new EmployeeRosterDto
            {
                Id = x.Id,
                EmployeeId = x.EmployeeId,
                FromDate = x.FromDate,
                ToDate = x.ToDate,
                InTime = x.InTime,
                OutTime = x.OutTime,
                EmployeeName = x.Employee.EmployeeName
            }).ToListAsync();
            var roster = rosters.FirstOrDefault(x => x.Id == id);
            if(roster != null)
            {
                return roster;
            }
            return null;
        }

        

        public async Task<EmployeeRoster> Update(int id, EmployeeRoster obj)
        {
            var rosterDb = await _Context.EmployeeRosters.FindAsync(id);
            if (rosterDb != null)
            {
                rosterDb.FromDate = obj.FromDate;
                rosterDb.ToDate = obj.ToDate;
                rosterDb.InTime = obj.InTime;
                rosterDb.OutTime = obj.OutTime;
                _Context.EmployeeRosters.Update(rosterDb);
                await _Context.SaveChangesAsync();
                return rosterDb;
            }
            return null;
        }

        public async Task<IEnumerable<EmployeeRoster>> SearchByEmp(string employeeName)
        {
            if (!string.IsNullOrWhiteSpace(employeeName))
            {
                var employeeRoster = await _Context.EmployeeRosters.Include(x => x.Employee).Where(x => x.Employee.EmployeeName.Contains(employeeName)).ToListAsync();
                return employeeRoster;
            }
            return null;
        }
    }
}
