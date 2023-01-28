using Infinite.TaxiBookingSystem.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infinite.TaxiBookingSystem.API.Repositories
{
    public class CustomerRepository : IRepository<Customer>,IGetRepository<Customer>
    {
        private readonly ApplicationDbContext _Context;

        public CustomerRepository(ApplicationDbContext context)
        {
            _Context = context;
        }

        public async Task Create(Customer obj)
        {
            _Context.Customers.Add(obj);
            await _Context.SaveChangesAsync();
        }

        public async Task<Customer> Delete(int id)
        {
            var customerDb = await _Context.Customers.FindAsync(id);
            if (customerDb != null)
            {
                _Context.Customers.Remove(customerDb);
                await _Context.SaveChangesAsync();
                return customerDb;
            }
            return null;
        }

        public IEnumerable<Customer> GetAll()
        {
            return _Context.Customers.ToList();
        }

        public async Task<Customer> GetById(int id)
        {
            var customer = await _Context.Customers.FindAsync(id);
            if (customer != null)
            {
                return customer;
            }
            return null;
        }

        public async Task<Customer> Update(int id, Customer obj)
        {
            var customerDb = await _Context.Customers.FindAsync(id);
            if(customerDb != null)
            {
                customerDb.CustomerName = obj.CustomerName;
                customerDb.PhoneNo = obj.PhoneNo;
                customerDb.EmailId = obj.EmailId;
                customerDb.Address = obj.Address;
                _Context.Customers.Update(customerDb);
                await _Context.SaveChangesAsync();
                return customerDb;
            }
            return null;
        }
    }
}
