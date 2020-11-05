using System;
using System.Diagnostics.CodeAnalysis;

namespace ZabbixToItop.Models
{
    [ExcludeFromCodeCoverage]
    public class ItopException : Exception
    {
        public int ErrorCode { get; }
        public ItopException(string paramName, int errorCode) : base (paramName)    
        {
            ErrorCode = errorCode;
        }
    }
}