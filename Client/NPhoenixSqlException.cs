using System;

namespace NPhoenix.Client
{
    [Serializable]
    public class NPhoenixSqlException : Exception
    {
        internal NPhoenixSqlException(string message)
            : base(message)
        {   
        }

        internal NPhoenixSqlException(java.lang.Exception ex)
            : base(ex.getCause().Message)
        {
        }
    }
}