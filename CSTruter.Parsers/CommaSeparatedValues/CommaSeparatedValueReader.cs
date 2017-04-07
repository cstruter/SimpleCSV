using System.IO;

namespace CSTruter.Parsers.CommaSeparatedValues
{
    /// <summary>
    /// CSV StreamReader Class
    /// </summary>
    public class CommaSeparatedValueReader : ICommaSeparatedValueReader
    {
        #region Private Fields

        private string _path;

        #endregion

        #region Constructors

        public CommaSeparatedValueReader(string path)
        {
            _path = path;
        }

        #endregion

        #region Public Methods

        public string[] GetLines()
            => File.ReadAllLines(_path);
        
        #endregion

    }
}
