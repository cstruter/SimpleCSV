using CSTruter.Parsers.CommaSeparatedValues;
using System.IO;
using System;
using CSTruter.Repositories;
using CSTruter.Services;

namespace CSTruter.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            var csvReader = new CommaSeparatedValueReader("data.csv");
            var customerRepository = new CustomerRepository(csvReader);
            var customerBusinessRules = new CustomerService(customerRepository);
            var frequencyList = customerBusinessRules.GetFrequencyList();
            var sortedAddressList = customerBusinessRules.GetSortedAddressList();
            File.WriteAllLines("frequencyList.txt", frequencyList);
            File.WriteAllLines("addressList.txt", sortedAddressList);

            Console.WriteLine("Display Frequency List");
            Console.WriteLine(File.ReadAllText("frequencyList.txt"));

            Console.WriteLine("Display Address List Sorted");
            Console.WriteLine(File.ReadAllText("addressList.txt"));
            Console.ReadKey();
        }
    }
}