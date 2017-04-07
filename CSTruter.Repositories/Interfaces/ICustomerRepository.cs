using CSTruter.Repositories.BusinessObjects;
using System.Collections.Generic;

namespace CSTruter.Repositories.Interfaces
{
    /// <summary>
    /// Interface for retrieving customers
    /// </summary>
    public interface ICustomerRepository
    {
        List<Customer> GetCustomers();
    }
}
