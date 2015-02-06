namespace NPhoenix.Interop
{
    internal class Constants
    {
        internal class Classes
        {
            public const string DriverManager = "java.sql.DriverManager";
        }

        internal class Methods
        {
            public const string GetConnection = "getConnection";
            public const string CreateStatement = "createStatement";
            public const string ExecuteUpdate = "executeUpdate";
            public const string PrepareStatement = "prepareStatement";
            public const string ExecuteQuery = "executeQuery";
            public const string Next = "next";
            public const string GetString = "getString";
            public const string Close = "close";
            public const string Commit = "commit";
        }

        internal class MethodSignatures
        {
            public const string GetConnection_String_Connection = "(Ljava/lang/String;)Ljava/sql/Connection;";
            public const string CreateStatement_Statement = "()Ljava/sql/Statement;";
            public const string ExecuteUpdate_String_Int = "(Ljava/lang/String;)I";
            public const string PreparedStatement_String_PreparedStatement = "(Ljava/lang/String;)Ljava/sql/PreparedStatement;";
            public const string ExecuteQuery_ResultSet = "()Ljava/sql/ResultSet;";
            public const string Bool = "()Z";
            public const string Void = "()V";
            public const string String_String = "(Ljava/lang/String;)Ljava/lang/String;";
        }
    }
}