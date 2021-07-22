using System.Linq;
using System.Threading.Tasks;
using GroveAirlines.DatabaseLayer;
using GroveAirlines.DatabaseLayer.Models;
using GroveAirlines.Exceptions;
using GroveAirlines.RepositoryLayer;
using GroveAirlines_Tests.Stubs;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GroveAirlines_Tests.RepositoryLayer
{
    [TestClass]
    public class CustomerRepositoryTests
    {
        private GroveAirlinesContext _context;
        private CustomerRepository _repository;

        [TestInitialize]
        public async Task TestInitialize()
        {
            DbContextOptions<GroveAirlinesContext> dbContextOptions =   // in memory temp database
                new DbContextOptionsBuilder<GroveAirlinesContext>().UseInMemoryDatabase("Grove").Options;
            _context = new GroveAirlinesContext_Stub(dbContextOptions);

            Customer testCustomer = new Customer("Benjamin Whelan");
            _context.Customer.Add(testCustomer);
            await _context.SaveChangesAsync();

            _repository = new CustomerRepository(_context);
            Assert.IsNotNull(_repository);
        }

        [TestMethod]
        public async Task CreateCustomer_Success()
        {   
            bool result = await _repository.CreateCustomer("Benjamin Whelan");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task CreateCustomer_Failure_NameIsNull()
        {
            bool resultNull = await _repository.CreateCustomer(null); // ""
            Assert.IsFalse(resultNull);
        }

        [TestMethod]
        public async Task CreateCustomer_Failure_NameIsEmptyString()
        {
            bool resultEmpty = await _repository.CreateCustomer(string.Empty);
            Assert.IsFalse(resultEmpty);
        }

        [TestMethod]
        [DataRow('!')]
        [DataRow('@')]
        [DataRow('#')]
        [DataRow('$')]
        [DataRow('%')]
        [DataRow('&')]
        [DataRow('*')]
        public async Task CreateCustomer_Failure_NameContainsInvalidCharacters(char invalidCharacter)
        {
            bool result = await _repository.CreateCustomer("Benjamin Whelan" + invalidCharacter);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task CreateCustomer_Failure_DatabaseAccessError()
        {
            CustomerRepository repository = new CustomerRepository(null);
            Assert.IsNotNull(repository);

            bool result = await repository.CreateCustomer("Donald Knuth");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task GetCustomerByName_Success()
        {
            Customer customer = await _repository.GetCustomerByName("Benjamin Whelan");
            Assert.IsNotNull(customer);

            Assert.AreEqual("Benjamin Whelan", customer.Name);
        }

        [TestMethod]
        [DataRow("")]
        [DataRow(null)]
        [DataRow("#")]
        [DataRow("$")]
        [DataRow("%")]
        [DataRow("&")]
        [DataRow("*")]
        [ExpectedException(typeof(CustomerNotFoundException))]  // public only
        public async Task GetCustomerByName_Failure_InvalidName(string name)
        {
            await _repository.GetCustomerByName(name);
        }

    }
}