using CSTruter.Repositories.BusinessObjects;
using CSTruter.Repositories.Interfaces;
using CSTruter.Services;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;

namespace ClassLibrary1
{
    [TestFixture(Description = "Test Customer Business Layer")]
    public class CustomersTests
    {
        #region Test Case Source Data

        protected static object[] FirstLastNameScenario =
        {
            new object[] { // Test if first and last name values are equal in a list
                new List<Customer>(new Customer[] {
                    new Customer { FirstName = "De Wet", LastName="De Wet" },
                    new Customer { FirstName = "Pietie", LastName="De Wet" }
                }), "De Wet,3,Pietie,1"
            },
            new object[] { // Normal scenario
                new List<Customer>(new Customer[] {
                    new Customer { FirstName = "Christoff", LastName="Truter" },
                    new Customer { FirstName = "Jurgens", LastName="Truter" }
                }), "Truter,2,Christoff,1,Jurgens,1"
            }
        };

        public static object[] AddressListTakeStreetNumberInAccountSorting =
        {
            new object[]
            { // Should be sorted according to street names
                new List<Customer>(new Customer[] {
                    new Customer { Address = "101 Chrome Street" },
                    new Customer { Address = "201 Albatros Street" },
                    new Customer { Address = "3 Berg Street" }
                }), "201 Albatros Street,3 Berg Street,101 Chrome Street"
            },
            new object[]
            { // Should be sorted according to street names and then numbers (if the same)
                new List<Customer>(new Customer[] {
                    new Customer { Address = "50 Chrome Street" },
                    new Customer { Address = "10 Chrome Street" },
                    new Customer { Address = "60 Chrome Street" }
                }), "10 Chrome Street,50 Chrome Street,60 Chrome Street"
            }
        };

        #endregion

        [Test(Description ="Sort by frequency of first/last names count and then the actual first/last name"), 
            TestCaseSource("FirstLastNameScenario")]
        public void GetFrequencyList_Given_MultipleTestCases(List<Customer> customers, string expected)
        {
            // Arrange
            var customerRepository = Substitute.For<ICustomerRepository>();
            customerRepository.GetCustomers().Returns(customers);
            var customerBusinessRules = new CustomerService(customerRepository);

            // Act
            var items = customerBusinessRules.GetFrequencyList();
            var list = new List<string>();
            foreach (var item in items)
                list.Add(item);
            var actual = string.Join(",", list.ToArray());

            // Assert
            StringAssert.AreEqualIgnoringCase(expected, actual);
        }

        [Test(Description = "When sorting addresses, the street number must be taken into account"), 
            TestCaseSource("AddressListTakeStreetNumberInAccountSorting")]
        public void GetSortedAddressList_Given_MultipleTestCases(List<Customer> customers, string expected)
        {
            // Arrange
            var customerRepository = Substitute.For<ICustomerRepository>();
            customerRepository.GetCustomers().Returns(customers);
            var customerBusinessRules = new CustomerService(customerRepository);

            // Act
            var items = customerBusinessRules.GetSortedAddressList();
            var list = new List<string>();
            foreach (var item in items)
                list.Add(item);
            var actual = string.Join(",", list.ToArray());

            // Assert
            StringAssert.AreEqualIgnoringCase(expected, actual);
        }
    }
}