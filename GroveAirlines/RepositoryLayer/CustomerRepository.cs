using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
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

        public CustomerRepository(GroveAirlinesContext context)
        {
            this._context = context;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]  // parameter less constructor only for testing
        public CustomerRepository()
        {
            if (Assembly.GetExecutingAssembly().FullName == Assembly.GetCallingAssembly().FullName)
            {
                throw new Exception("This constructor should only be used for testing");
            }
        }

        public async Task<bool> CreateCustomer(string name) // async Task<>
        {
            if (IsInvalidCustomerName(name))
            {
                return false;
            }

            try
            {
                var newCustomer = new Customer(name);
                await using (_context)
                {
                    _context.Customer.Add(newCustomer);
                    await _context.SaveChangesAsync();
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        public virtual async Task<Customer> GetCustomerByName(string name)
        {
            if (IsInvalidCustomerName(name))
            {
                throw new CustomerNotFoundException();
            }
                // DbSet<Customer> Access
            return await _context.Customer.FirstOrDefaultAsync(c => c.Name == name)     // find matches between name props
                   ?? throw new CustomerNotFoundException();    // if NULL respond with this
        }

        private static bool IsInvalidCustomerName(string name)
        {
            char[] forbiddenCharacters = { '!', '@', '#', '$', '%', '&', '*', '=' };    // TODO: Check more characters.
            return string.IsNullOrEmpty(name) || name.Any(x => forbiddenCharacters.Contains(x));
        }
    }
}
