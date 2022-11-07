using System;

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