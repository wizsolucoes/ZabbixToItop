using System;
namespace ZabbixToItop.Models
{
    public class ItopException : Exception
    {
        public int ErrorCode { get; }
        public ItopException(string paramName, int errorCode) : base (paramName)    
        {
            ErrorCode = errorCode;
        }
    }
}