namespace CSTruter.Parsers.CommaSeparatedValues
{
    /// <summary>
    /// Interface a CSV reader needs to implement in order to return raw CSV content
    /// </summary>
    public interface ICommaSeparatedValueReader
    {
        string[] GetLines();
    }
}
