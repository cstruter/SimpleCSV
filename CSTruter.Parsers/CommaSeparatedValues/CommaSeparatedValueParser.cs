using System.Collections.Generic;
using System.Reflection;

namespace CSTruter.Parsers.CommaSeparatedValues
{
    /// <summary>
    /// Simple rudimentary class used for parsing CSV files
    /// </summary>
    public class CommaSeparatedValueParser
    {
        #region Private Fields

        private char _separator;
        private ICommaSeparatedValueReader _reader;

        #endregion

        #region Constructors

        public CommaSeparatedValueParser(ICommaSeparatedValueReader reader, char separator)
        {
            _reader = reader;
            _separator = separator;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Convert CSV resource to a dictionary list
        /// </summary>
        /// <returns></returns>
        public List<Dictionary<string, string>> ToDictionaryList()
        {
            string[] lines = _reader.GetLines();
            if (lines.Length == 0)
                return null;
            string header = lines[0];
            string[] headerFields = header.Split(new char[] { _separator });
            var list = new List<Dictionary<string, string>>();
            int totalLines = lines.Length;
            for (var i = 1; i < totalLines; i++)
            {
                var item = toDictionary(lines[i], headerFields);
                list.Add(item);
            }
            return list;
        }

        /// <summary>
        /// Convert CSV resource to a generic object list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> ToObjectList<T>() where T : new()
        {
            var objectList = new List<T>();
            var dictionaryList = ToDictionaryList();
            if (dictionaryList == null)
                return null;
            foreach (var item in dictionaryList)
            {
                var result = dictionaryToObject<T>(item);
                objectList.Add(result);
            }
            return objectList;
        }

        #endregion

        #region Private Methods

        private T dictionaryToObject<T>(Dictionary<string, string> item) where T : new()
        {
            var result = new T();
            var properties = result.GetType().GetProperties();

            foreach (var property in properties)
            {
                var attribute = property.GetCustomAttribute<CommaSeparatedValueFieldAttribute>();
                if (attribute != null)
                {
                    if (item.ContainsKey(attribute.PropertyName))
                    {
                        property.SetValue(result, item[attribute.PropertyName]);
                    }
                }
            }

            return result;
        }

        private Dictionary<string, string> toDictionary(string line, string[] headerFields)
        {
            int totalColumns = headerFields.Length;
            var item = new Dictionary<string, string>();
            string[] valueFields = line.Split(new char[] { _separator });
            if (totalColumns != valueFields.Length)
                throw new CommaSeparatedValueException($"Column mismatch, {totalColumns} column(s) specified, {valueFields.Length} cells found", 1000);
            for (var j = 0; j < totalColumns; j++)
            {
                string key = headerFields[j];
                string value = valueFields[j];
                item.Add(key, value);
            }
            return item;
        }

        #endregion
    }
}