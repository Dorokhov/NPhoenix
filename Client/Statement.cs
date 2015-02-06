using System;
using System.Diagnostics.Contracts;
using java.lang.reflect;
using net.sf.jni4net.jni;
using NPhoenix.Interop;

namespace NPhoenix.Client
{
    /// <summary>
    /// The object used for executing a static SQL statement and returning the results it produces. 
    /// </summary>
    public class Statement : IDisposable
    {
        private readonly java.lang.Object _statement;

        internal Statement(java.lang.Object statement)
        {
            Contract.Requires(statement != null);
            _statement = statement;
        }

        /// <summary>
        /// Executes the given SQL statement, which may be an INSERT, UPDATE, or DELETE statement or an SQL statement that returns nothing, such as an SQL DDL statement. 
        /// </summary>
        /// <param name="query"></param>
        public void ExecuteUpdate(string query)
        {
            try
            {
                Method executeUpdate = _statement.GetExecuteUpdate();
                executeUpdate.invoke(_statement, new[] { JNIEnv.ThreadEnv.NewString(query) });
            }
            catch (java.lang.Exception ex)
            {
                throw new NPhoenixSqlException(ex);
            }
        }

        /// <summary>
        /// Releases this  <see cref="Statement"/> object's database and JDBC resources immediately instead of waiting for this to happen when it is automatically closed. 
        /// It is generally good practice to release resources as soon as you are finished with them to avoid tying up database resources.
        /// </summary>
        public void Close()
        {
            _statement.Close();
        }

        /// <summary>
        /// Releases this  <see cref="Statement"/> object's database and JDBC resources immediately instead of waiting for this to happen when it is automatically closed. 
        /// It is generally good practice to release resources as soon as you are finished with them to avoid tying up database resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Close();
            }
        }
    }
}
