using System;

namespace CSTruter.Parsers.CommaSeparatedValues
{
    /// <summary>
    /// Decorate properties with this attribute in order to map them to CSV field values 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class CommaSeparatedValueFieldAttribute : Attribute
    {
        #region Public Properties

        /// <summary>
        /// Property name used to indicate field mapping
        /// </summary>
        public string PropertyName { get; }

        #endregion

        #region Constructors

        public CommaSeparatedValueFieldAttribute(string propertyName)
        {
            PropertyName = propertyName;
        }

        #endregion
    }
}
