using CSTruter.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace CSTruter.Services
{
    /// <summary>
    /// Class used for retrieving customer related information
    /// </summary>
    public class CustomerService
    {
        private ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        /// <summary>
        /// Get a list of customer first or last names ordered by frequency (descending) and name (ascending)
        /// </summary>
        /// <param name="data">list of customers</param>
        /// <returns></returns>
        public IEnumerable<string> GetFrequencyList()
        {
            var data = _customerRepository.GetCustomers();
            return data?.Select(c => c.FirstName).Concat(data.Select(c => c.LastName))
                .GroupBy(c => c)
                .Select(c => new { Name = c.Key, Count = c.Count() })
                .OrderByDescending(c => c.Count)
                .ThenBy(c => c.Name)
                .Select(c => $"{c.Name},{c.Count}");
        }
        /// <summary>
        /// Get a list of addresses ordered by street name
        /// </summary>
        /// <param name="data">list of customers</param>
        /// <returns></returns>
        public IEnumerable<string> GetSortedAddressList()
        {
            var data = _customerRepository.GetCustomers();
            return data?.Select(c => new { c.StreetNumber, c.Address })
                .OrderBy(c => c.Address)
                .ThenBy(c=> c.StreetNumber)
                .Select(c => $"{c.StreetNumber} {c.Address}");
        }
    }
}
