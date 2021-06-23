using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroveAirlines.DatabaseLayer;
using GroveAirlines.DatabaseLayer.Models;
using GroveAirlines.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace GroveAirlines.RepositoryLayer
{
    public class CustomerRepository
    {
        private readonly GroveAirlinesContext _context;

        public CustomerRepository(GroveAirlinesContext _context)
        {
            this._context = _context;
        }

        public CustomerRepository()
        {

        }

        public async Task<bool> CreateCustomer(string name) // async Task<>
        {
            if (IsInvalidCustomerName(name))
            {
                return false;
            }

            Customer newCustomer = new Customer(name);
            await using GroveAirlinesContext context = new GroveAirlinesContext();
            context.Customer.Add(newCustomer);
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<Customer> GetCustomerByName(string name)
        {
            if (IsInvalidCustomerName(name))
            {
                throw new CustomerNotFoundException();
            }
                // DbSet<Customer> Access
            return await _context.Customer.FirstOrDefaultAsync(c => c.Name == name)     // find matches between name props
                   ?? throw new CustomerNotFoundException();    // if NULL respond with this
        }

        private bool IsInvalidCustomerName(string name)
        {
            char[] forbiddenCharacters = { '!', '@', '#', '$', '%', '&', '*' };
            return string.IsNullOrEmpty(name) || name.Any(x => forbiddenCharacters.Contains(x));
        }
    }
}
