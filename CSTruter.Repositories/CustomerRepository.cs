using CSTruter.Repositories.Interfaces;
using System.Collections.Generic;
using CSTruter.Repositories.BusinessObjects;
using CSTruter.Parsers.CommaSeparatedValues;

namespace CSTruter.Repositories
{
    /// <summary>
    /// Retrieve customer information from a CSV file
    /// </summary>
    public class CustomerRepository : ICustomerRepository
    {
        private ICommaSeparatedValueReader _reader;

        public CustomerRepository(ICommaSeparatedValueReader reader)
        {
            _reader = reader;
        }

        /// <summary>
        /// Get a list of customers from a CSV file
        /// </summary>
        /// <param name="path">path to filename</param>
        /// <returns></returns>
        public List<Customer> GetCustomers()
        {
            var parser = new CommaSeparatedValueParser(_reader, ',');
            return parser.ToObjectList<Customer>();
        }
    }
}
