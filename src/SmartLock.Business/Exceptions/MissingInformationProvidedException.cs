using System;

namespace SmartLock.Business.Exceptions
{
    public class MissingInformationProvidedException : Exception
    {
        public MissingInformationProvidedException(string message) : base(message)
        {
        }
    }
}