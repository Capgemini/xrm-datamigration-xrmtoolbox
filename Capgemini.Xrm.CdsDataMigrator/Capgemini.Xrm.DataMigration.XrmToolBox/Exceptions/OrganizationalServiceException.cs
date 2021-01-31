using System;
using System.Runtime.Serialization;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Exceptions
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

        public OrganizationalServiceException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected OrganizationalServiceException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}