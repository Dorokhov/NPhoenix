using System;
using System.Diagnostics.Contracts;
using net.sf.jni4net.jni;
using NPhoenix.Interop;
using Object = java.lang.Object;

namespace NPhoenix.Client
{
    /// <summary>
    /// Provides connection to phoenix server.
    /// You have to specify URL in format "jdbc:phoenix:[your zookeeper server name]:[port]"
    /// [port] is optional and default value is 2181.
    /// </summary>
    public class NPhoenixConnection : IDisposable
    {
        private readonly string _url;
        private Object _connection;

        public NPhoenixConnection(string url)
        {
            Contract.Requires(!string.IsNullOrEmpty(url));

            _url = url;
        }

        /// <summary>
        /// Attempts to establish connection to a phoenix server.
        /// Setups .NET-Java bridge before connect to a server.
        /// </summary>
        public void Connect()
        {
            PlatformBridge.Setup();
            _connection = DriverReflection.GetConnection().invoke(null, new Object[]
            {
                JNIEnv.ThreadEnv.NewString(_url)
            });
        }

        /// <summary>
        /// Creates a <see cref="Statement"/> object for sending SQL statements to the database. 
        /// SQL statements without parameters are normally executed using Statement objects. 
        /// If the same SQL statement is executed many times, it may be more efficient to use a <see cref="PreparedStatement"/> object. 
        /// </summary>
        /// <returns>a new default <see cref="Statement"/> object</returns>
        public Statement CreateStatement()
        {
            return new Statement(_connection.CreateStatement());
        }

        /// <summary>
        /// Creates a <see cref="PreparedStatement"/> object for sending parameterized SQL statements to the database. 
        /// A SQL statement with or without IN parameters can be pre-compiled and stored in a <see cref="PreparedStatement"/> object. 
        /// This object can then be used to efficiently execute this statement multiple times. 
        /// </summary>
        /// <returns></returns>
        public PreparedStatement PrepareStatement(string query)
        {
            return new PreparedStatement(_connection.PrepareStatement(query));
        }

        /// <summary>
        /// Releases this <see cref="NPhoenixConnection"/> object's database and JDBC resources immediately instead of waiting for them to be automatically released. 
        /// </summary>
        public void Close()
        {
            _connection.Close();
        }

        /// <summary>
        /// Makes all changes made since the previous commit/rollback permanent and releases any database locks currently held by this <see cref="NPhoenixConnection"/> object. 
        /// This method should be used only when auto-commit mode has been disabled.
        /// </summary>
        public void Commit()
        {
            _connection.Commit();
        }

        /// <summary>
        /// Releases this <see cref="NPhoenixConnection"/> object's database and JDBC resources immediately instead of waiting for them to be automatically released. 
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