using System;

namespace CSTruter.Parsers.CommaSeparatedValues
{
    public class CommaSeparatedValueException : Exception
    {
        public int Code { get; }

        public CommaSeparatedValueException(string message, int code) : base(message) { Code = code; }
    }
}
