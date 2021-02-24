using System;

namespace Application.Common.Exceptions
{
    public class InternalServerException : Exception
    {
        public InternalServerException(string exceptionMessage)
            :base(exceptionMessage) { }
    }
    
}