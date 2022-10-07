using System;
using System.Runtime.Serialization;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Exceptions
{
    [Serializable]
    public class OrganizationalServiceException : Exception
    {
        public OrganizationalServiceException()
        {
        }

        public OrganizationalServiceException(string message)
            : base(message)
        {
        }
    }
}