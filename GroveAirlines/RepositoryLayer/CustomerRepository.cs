using System;
using System.Collections.Generic;
using System.Linq;
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

        public CustomerRepository(GroveAirlinesContext _context)
        {
            this._context = _context;
        }

        public async Task<bool> CreateCustomer(string name) // async Task<>
        {
            if (IsInvalidCustomerName(name))
            {
                return false;
            }

            try
            {
                Customer newCustomer = new Customer(name);
                using (_context)
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
            char[] forbiddenCharacters = { '!', '@', '#', '$', '%', '&', '*', '=' };    // TODO: Check more characters.
            return string.IsNullOrEmpty(name) || name.Any(x => forbiddenCharacters.Contains(x));
        }
    }

    //internal class CustomerEqualityComparer : EqualityComparer<Customer>
    //{
    //    public override bool Equals(Customer? x, Customer? y)   // overriding abstract method
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override int GetHashCode(Customer obj)
    //    {   // class avoids security issue non-pseudo-random number generator
    //        int randomNumber = RandomNumberGenerator.GetInt32(int.MaxValue / 2);    // creation of rdm number
    //        return (obj.CustomerId + obj.Name.Length + randomNumber).GetHashCode();    // hashing
    //    }
    //}
}
