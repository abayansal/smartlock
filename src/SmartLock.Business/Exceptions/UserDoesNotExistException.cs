using System;

namespace SmartLock.Business.Exceptions
{
    public class UserDoesNotExistException : Exception
    {
        public UserDoesNotExistException(string message) : base(message)
        {
        }
    }
}