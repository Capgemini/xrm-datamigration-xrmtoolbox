using System;
using System.Runtime.Serialization;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Exceptions
{
    [Serializable]
    public class MappingException : Exception
    {
        public MappingException()
        {
        }

        public MappingException(string message)
            : base(message)
        {
        }

        public MappingException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected MappingException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}