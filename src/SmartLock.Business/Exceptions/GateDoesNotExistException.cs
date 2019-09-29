using System;

namespace SmartLock.Business.Exceptions
{
    public class GateDoesNotExistException : Exception
    {
        public GateDoesNotExistException(string message) : base(message)
        {
        }
    }
}
