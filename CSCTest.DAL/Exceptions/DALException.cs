using System;

namespace CSCTest.DAL.Exceptions
{
    public class DALException : Exception
    {
        public DALException(string message) : base(message)
        {
            
        }
    }
}