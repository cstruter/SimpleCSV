using CSTruter.Parsers.CommaSeparatedValues;
using CSTruter.Repositories.BusinessObjects;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;

namespace CSTruter.TestLibrary
{
    [TestFixture(Description = "Test Comma Separated Value Parser")]
    public class CommaSeparatedValueParserTests
    {
        protected static object[] ObjectMappingTestSource =
        {
            new object[] {
                new string[] { "FirstName,LastName", "Christoff,Truter" },
                "FirstName:Christoff,LastName:Truter"
            },
            new object[]
            {
                new string[] { "FirstName,LastName", "Maree,Kleu", "Gerhardt,Stander", "Christoff,Truter" },
                "FirstName:Maree,LastName:Kleu,FirstName:Gerhardt,LastName:Stander,FirstName:Christoff,LastName:Truter"
            }
        };

        protected static object[] ObjectMappingTestSourceLessColumnsOrFields =
        {
            new object[] { // More cells
                new string[] { "FirstName, LastName", "Christoff,Truter,Dude" },
            },
            new object[] { // Less cells
                 new string[] { "FirstName, LastName", "Christoff" }
            }
        };

        [Test(Description = "Simple Mapping Tests, just checking if decorated data does in fact get mapped"),
            TestCaseSource("ObjectMappingTestSource")]
        public void ToObjectList_Given_MultipleTestCases(string[] lines, string expected)
        {
            // Arrange
            var reader = Substitute.For<ICommaSeparatedValueReader>();
            reader.GetLines().Returns(lines);
            var parser = new CommaSeparatedValueParser(reader, ',');

            // Act
            var customers = parser.ToObjectList<Customer>();
            var actual = CustomersToString(customers);

            // Assert
            StringAssert.AreEqualIgnoringCase(expected, actual);
        }

        [Test(Description = "When an incorrect number of columns or fields are supplied, the object must throw an exception"),
            TestCaseSource("ObjectMappingTestSourceLessColumnsOrFields")]
        public void ToObjectList_Given_LessColumnsOrFields_ShowThrowException(string[] lines)
        {
            // Arrange
            var reader = Substitute.For<ICommaSeparatedValueReader>();
            reader.GetLines().Returns(lines);
            var parser = new CommaSeparatedValueParser(reader, ',');

            // Assert
            Assert.Throws<CommaSeparatedValueException>(() => {
                // Act
                parser.ToObjectList<Customer>();
            });
        }

        protected string CustomersToString(List<Customer> customers)
        {
            var list = new List<string>();
            foreach (var customer in customers)
            {
                if (!string.IsNullOrEmpty(customer.Address))
                    list.Add($"Address:{customer.Address}");
                if (!string.IsNullOrEmpty(customer.FirstName))
                    list.Add($"FirstName:{customer.FirstName}");
                if (!string.IsNullOrEmpty(customer.LastName))
                    list.Add($"LastName:{customer.LastName}");
                if (!string.IsNullOrEmpty(customer.PhoneNumber))
                    list.Add($"PhoneNumber:{customer.PhoneNumber}");
                if (!string.IsNullOrEmpty(customer.StreetNumber))
                    list.Add($"StreetNumber:{customer.StreetNumber}");
            }
            return string.Join(",", list.ToArray());
        }
    }
}