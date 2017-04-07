using CSTruter.Parsers.CommaSeparatedValues;
using System.Linq;

namespace CSTruter.Repositories.BusinessObjects
{
    /// <summary>
    /// Customer Mapped Class
    /// </summary>
    public class Customer
    {
        #region Private Fields

        private string _Address;
        private string _StreetNumber;

        #endregion

        #region Public Properties

        [CommaSeparatedValueField("FirstName")]
        public string FirstName { get; set; }

        [CommaSeparatedValueField("LastName")]
        public string LastName { get; set; }

        public string StreetNumber
        {
            get
            {
                return _StreetNumber;
            }
        }

        [CommaSeparatedValueField("Address")]
        public string Address
        {
            get
            {
                return _Address;
            }
            set
            {
                // The street number needs to be stripped from the address as a sorting requirement
                if (!string.IsNullOrEmpty(value))
                {
                    var fragments = value.Split();
                    int streetNumber;
                    if (int.TryParse(fragments[0], out streetNumber))
                    {
                        _StreetNumber = streetNumber.ToString();
                        _Address = string.Join(" ", fragments.Skip(1).ToArray());
                        return;
                    }
                }
                _Address = value;
            }
        }

        [CommaSeparatedValueField("PhoneNumber")]
        public string PhoneNumber { get; set; }

        #endregion
    }
}
